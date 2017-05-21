using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QLPC.Models.BussinessModel;
using QLPC.Models.DataModel;
using PagedList;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace QLPC.Controllers
{

    public class HomeController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        public ActionResult Index() {
            KHACHHANG kh = db.khachhang.First();

            //GoiY.KiemTra();
            var pcBanChays = db.Database.SqlQuery<SanPhamStore>("SP_SanPhamBanChay").ToList();
            ViewBag.pcBanChay = pcBanChays;
            var pcMoiNhats = db.Database.SqlQuery<SanPhamStore>("SP_SanPhamMoi").ToList();
            ViewBag.pcMoiNhat = pcMoiNhats;
            if (Session["SanPhamXem"] != null)
            {
                var pcGoiYXem = GoiY.TimTheoXem(Session["SanPhamXem"] as string);
                ViewBag.pcGoiYXem = pcGoiYXem;
                ViewBag.Count2 = pcGoiYXem.Count();
            }
            if (Session["MAKH"] != null)
            {
                var pcGoiY = GoiY.TimTheoNguoiDung((int)Session["MAKH"]).ToList();
                ViewBag.pcGoiY = pcGoiY;
                ViewBag.Count = pcGoiY.Count();
            }


            return View();



        }

        public ActionResult SanPhamTheoHang(string id, int? page)
        {
            try
            {
                var sanpham = db.Database.SqlQuery<SanPhamStore>("SP_SanPhamTheoHang @hang", new SqlParameter("@hang", id)).ToList();
                var pageNumber = page ?? 1;
                var onePageOfSanpham = sanpham.ToPagedList(pageNumber, 16);
                ViewBag.sanpham = onePageOfSanpham;
                ViewBag.Hang = id;
                ViewBag.Gia = 0;
                ViewBag.so = 0;
                ViewBag.SoMay = sanpham.Count();
                return View();
            }
            catch
            {
               return RedirectToAction("Index");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult ReviewProduct(string id)
        {
            try
            {

                var sanpham = db.sanpham.Where(x => x.MODEL == id).First();
                ViewBag.sanpham = sanpham;
                var cauhinh = db.linhkien.Where(x => x.SERIAL == sanpham.SERIAL).OrderBy(x=>x.TENLOAI);
                ViewBag.cauhinh = cauhinh;
                if(Session["MAKH"] == null)
                {
                    var goiYTheoSanPham = GoiY.TimTheoSanPham(id).ToList();
                    ViewBag.goiYTheoSanPham = goiYTheoSanPham;
                }
                else
                {
                    var goiYTheoSanPham = GoiY.GhepGoiY((int)Session["MAKH"], id).ToList();
                    ViewBag.goiYTheoSanPham = goiYTheoSanPham;
                }
                if (Session["MAKH"]==null)
                {
                    string chuoiSanPham = Session["SanPhamXem"] as string + id + ";";
                    Session["SanPhamXem"] = chuoiSanPham;
                }
                return View();
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult FilterView(string hang, int gia, int sapxep,int? page)
        {
            try
            {
                var sanpham = db.Database.SqlQuery<SanPhamStore>("SP_LocSanPham @hang, @gia, @sapxep", new SqlParameter("@hang",hang),new SqlParameter("@gia",gia), new SqlParameter("@sapxep",sapxep)).ToList();
                var pageNumber = page ?? 1;
                var onePageOfSanpham = sanpham.ToPagedList(pageNumber, 16);
                ViewBag.sanpham = onePageOfSanpham;
                ViewBag.Hang = hang;
                ViewBag.Gia = gia != 0 ? gia : 0;
                ViewBag.SoMay = sanpham.Count();
                ViewBag.so = 1;
                return View("SanPhamTheoHang");               
            }
            catch
            {
                return View("Index");
            }
        }

        public ActionResult DangNhap(string uname2, string psw2)
        {
            var khachhang = db.khachhang.SingleOrDefault(x => x.DTHOAIKH == uname2 && x.PASS == psw2);
            if(khachhang != null)
            {
                Session["MAKH"] = khachhang.MAKH;
                Session["SDT"] = khachhang.DTHOAIKH;
                Session["TenKhachHang"] = khachhang.TENKH;
                Session["DiaChi"] = khachhang.DCHIKH;
                int SoHangTrongGio= db.giohang.Where(x => x.MAKH == khachhang.MAKH).Count();
                Session["SoHangGioHang"] = SoHangTrongGio;
                Session["SanPhamXem"] = null;
                RedirectToAction("Index");
            }
            ViewBag.Error = "Tài khoản mật khẩu không đúng!";
            ViewBag.show = 1;
            return RedirectToAction("Index"); ;
        }

        public ActionResult DangXuat()
        {
            Session["SDT"] = null;
            Session["SoHangGioHang"] = null;
            Session["TenKhachHang"] =null;
            Session["DiaChi"] = null;
            Session["MAKH"] = null;
            Session["SanPhamXem"] = null;
            return RedirectToAction("Index");
        }


        public ActionResult DangKi(string hoten, string sdt, string psw, string diachi)
        {
            try
            {
                KHACHHANG kh = new KHACHHANG();
                kh.TENKH = hoten;
                kh.DTHOAIKH = sdt;
                kh.PASS = psw;
                kh.DCHIKH = diachi;
                kh.TINH = "Can Tho";
                kh.EMAIL = "pepchit01@gmail.com";
                db.khachhang.Add(kh);
                db.SaveChanges();
                ViewBag.ThongBao = "Đăng kí thành công";
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }


      public ActionResult XemGioHang()
        {
            try
            {
                 if (Session["MAKH"] != null)
            {
                int tongTien = 0;
                var sanPhamTrongGios = db.Database.SqlQuery<SanPhamStore>("SP_LayGioHang @ma", new SqlParameter("@ma", Session["MAKH"])).ToList();
                foreach (var item in sanPhamTrongGios)
                {
                    tongTien = tongTien + (int)item.GIA;
                }
                ViewBag.TongTien = tongTien;
                ViewBag.sanPhamTrongGio = sanPhamTrongGios;
                ViewBag.Dem = sanPhamTrongGios.Count();
                return View();
            }
            else
            {
                ViewBag.Dem = 0;
                return View();
            }
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult GioHang(string id)
        {
            try
            {
                var sanpham = db.sanpham.Where(x => x.MODEL == id).Take(1).ToList();
                var a=db.Database.SqlQuery<int>("ThemGio @ma, @model", new SqlParameter("@ma", (int)Session["MAKH"]),new SqlParameter("@model", sanpham[0].SERIAL)).ToList();
                int ma = (int)Session["MAKH"];
                Session["SoHangGioHang"] = (int)Session["SoHangGioHang"] + 1;
                return RedirectToAction("XemGioHang");
             }
            catch
            {
                return RedirectToAction("Index");
             }
         }

        public JsonResult DeleteCart(string model)
        {
            try
            {
                List<GIOHANG> a = db.giohang.Where(x => x.MAKH == (int)Session["MAKH"]).ToList();
                db.giohang.Remove(a[0]);
                db.SaveChanges();
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                throw;
            }
        }

       public ActionResult ThanhToan()
        {
            List<string> gio = db.Database.SqlQuery<string>("SP_LayGio @ma", new SqlParameter("@ma", (int)Session["MAKH"])).ToList();
            foreach(var item in gio)
            {
                var exec = db.Database.SqlQuery<int>("SP_ThanhToan @ma,@serial ",new SqlParameter("@ma", (int)Session["MAKH"]), new SqlParameter("@serial", item)).ToList();
                var exec1= db.Database.SqlQuery<int>("SP_DeleteGio @ma, @serial", new SqlParameter("@ma", (int)Session["MAKH"]),new SqlParameter("@serial", item)).ToList();
            }
            TempData["ThongBaoMua"] = "Đã thanh toán thành công! Hàng sẽ được chuyển trong vòng 24h!";
            Session["SoHangGioHang"] = 0;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGio(string id)
        {
            List<SANPHAM> item = db.sanpham.Where(x => x.MODEL == id).ToList();
            var exec1 = db.Database.SqlQuery<int>("SP_DeleteGio @ma, @serial", new SqlParameter("@ma", (int)Session["MAKH"]), new SqlParameter("@serial", item[0].SERIAL)).ToList();
            return RedirectToAction("XemGioHang");
        }

    }
}