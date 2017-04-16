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

namespace QLPC.Controllers
{
    public class HomeController : Controller
    {
        private QLPCDbContext db = new QLPCDbContext();

        public ActionResult Index() {
            try {
                var pcBanChays = db.Database.SqlQuery<SanPhamStore>("SP_SanPhamBanChay").ToList();
                ViewBag.pcBanChay = pcBanChays;
                var pcMoiNhats = db.Database.SqlQuery<SanPhamStore>("SP_SanPhamMoi").ToList();
                ViewBag.pcMoiNhat = pcMoiNhats;
                return View();
            }
            catch
            {
                return View("/Shared/Error.cshtml");
            }

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

    }
}