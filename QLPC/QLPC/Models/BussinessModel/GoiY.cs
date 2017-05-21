using System;
using System.Collections.Generic;
using System.Linq;
using QLPC.Models.DataModel;
using System.Data.SqlClient;
using System.Data.Entity;
using Microsoft.Ajax.Utilities;
using System.Web.Services.Description;

namespace QLPC.Models.BussinessModel
{
    public class GoiY
    {
        //tập dữ liệu 13040 khách hàng, 1/4=3260 ,3/4=9780  13037 người
        public static QLPCDbContext db = new QLPCDbContext();

        public static object Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ma"></param>
        /// <returns></returns>
        public static List<SanPhamLoc> TimTheoNguoiDung(int ma)
        {
            //int ma = 50;
            List<KhachHangLoc> KH = new List<KhachHangLoc>();
            List<SanPhamLoc> SP = new List<SanPhamLoc>();
            var KHTam = db.Database.SqlQuery<int>("SP_LayKhachHang @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            foreach(var item in KHTam)
            {
                KH.Add(new KhachHangLoc() { Ma = item,perSon=0,trungBinh=0 });
            }
            var SPTam = db.Database.SqlQuery<SanPhamStore>("SP_LaySanPham @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            foreach(var item in SPTam)
            {
                SP.Add(new SanPhamLoc() {sanpham=item,pred=0 });
            }

            int soKhachHang = KH.Count(); //Số khách hàng có tại hệ thống
            int soSanPham = SP.Count();//Số sản phẩm có tại hệ thống
            List<int> giaoDich = new List<int>(); //List chứa giao dịch của tất cả các người dùng, trừ người dùng hiện tại
            List<int> giaoDichNguoi = new List<int>();//List chỉ chứa giao dịch của người dùng hiện tại
            //int t = 0;
            //var muaBan = db.muaban.Include(x => x.SANPHAM).ToList();
            var muaBan = db.Database.SqlQuery<SuMuaBan>("SP_LayMuaBan @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            for (int i = 0; i < soKhachHang; i++)
            {
                for (int j = 0; j < soSanPham; j++)
                {   
                    if (muaBan.Any(x => x.MAKH == KH[i].Ma && x.MODEL == SP[j].sanpham.MODEL))
                    {
                        giaoDich.Add(1);
                        if (KH[i].Ma == ma)
                            giaoDichNguoi.Add(1);
                    }
                    else
                    {
                        giaoDich.Add(0);
                        if (KH[i].Ma == ma)
                            giaoDichNguoi.Add(0);
                    }
                }
            }
            double trungBinhX = (double)0.0D;//Trung bình giao dịch người dùng hiện tại
            for (int i = 0; i < giaoDichNguoi.Count(); i++)//vòng for trungBinhX
            {
                trungBinhX = trungBinhX + (double)giaoDichNguoi[i] / soSanPham;
            }
            List<double> trungBinhY = new List<double>();//Danh sách trung bình giao dịch của mỗi người dùng
            double trungBinhTam = (double)0.0D;//biến tạm
            int dem = 1;
            for (int i = 0; i < giaoDich.Count(); i++)//Vòng for trungBinhY
            {
                trungBinhTam = trungBinhTam + (double)giaoDich[i] / (double)soSanPham;
                if (i == (soSanPham * dem) - 1)//kiểm tra vị trí kết thúc giao dịch của mỗi người dùng trong mảng
                {
                    trungBinhY.Add(trungBinhTam);
                    KH[dem-1].trungBinh = trungBinhTam;
                    trungBinhTam = (double)0.0D;
                    dem++;
                }
            }
            double tongTren = (double)0.0D; //Phần trên của công thức
            double tongDuoiX = (double)0.0D; // Phần dưới của công thức theo X
            double tongDuoiY = (double)0.0D;//Phần dưới của công thức theo Y
            double tongDuoi = (double)0.0D;//phần dưới của công thức tongDuoi = tongDuoiX+tongDuoiY
            dem = 1;// hệ số nhân xác định vị trí dừng trong mãng giaoDich
            int dem2 = 0;
            int dem3 = 0;
            for (int i = 0; i < giaoDich.Count(); i++)//chạy tất cả các giaoDich, tính tổng trên và tổng dưới để ra pearson
            {
                tongTren = tongTren + ((double)giaoDichNguoi[dem3] - trungBinhX) * ((double)giaoDich[i] - (double)trungBinhY[dem2]);
                tongDuoiX = tongDuoiX + Math.Pow(((double)giaoDichNguoi[dem3] - (double)trungBinhX), 2);
                tongDuoiY = tongDuoiY + Math.Pow(((double)giaoDich[i] - (double)trungBinhY[dem2]), 2);
                dem3++;
                if (i == (soSanPham * dem) - 1)//kiểm tra xem đến vị trí kết thúc giaoDich của người dùng
                {
                    tongDuoi = Math.Sqrt((double)tongDuoiX * (double)tongDuoiY);//tổng dưới tongDuoi = tongDuoiX+tongDuoiY
                    if (tongDuoi == 0)//Kiểm tra nếu tổng dưới bằng không thì person=0, trách trường hợp chia cho 0
                    {
                        KH[dem2].perSon = 0;
                    }
                    else
                    {
                        KH[dem2].perSon = ((double)tongTren / (double)(tongDuoi));
                    }
                    tongTren = (double)0.0D; // Reset lại các kết quả
                    tongDuoiX = (double)0.0D;
                    tongDuoiY = (double)0.0D;
                    tongDuoi = (double)0.0D;
                    dem++;
                    dem2++;
                    dem3 = 0;
                }
            }

            List<SanPhamLoc> sanPhamLoc = new List<SanPhamLoc>(); //danh sách các sản phẩm có khả năng cho việc gợi ý
            List<KhachHangLoc> khachHangLoc = new List<KhachHangLoc>();// danh sách khách hàng có pearson lớn hơn ngưỡng= 0.25
            for (int i = 0; i < KH.Count(); i++)
            {
                if (KH[i].perSon >= 0.25 && KH[i].Ma!=ma)
                {
                    khachHangLoc.Add(KH[i]);
                }
            }

            foreach (var item1 in khachHangLoc)
            {
                var tam = db.Database.SqlQuery<SanPhamStore>("SP_LocSanPhamGoiY @ma", new SqlParameter("@ma", item1.Ma)).ToList();
                foreach (var item2 in tam)
                {
                    sanPhamLoc.Add(new SanPhamLoc { sanpham = item2, pred = 0 });
                }
            }
            tongTren = 0;
            tongDuoi = 0;
            bool a;
            foreach (var item in sanPhamLoc)
            {
                for (int i = 0; i < khachHangLoc.Count(); i++)
                {
                    a = muaBan.Any(x => x.MAKH == khachHangLoc[i].Ma && x.MODEL == item.sanpham.MODEL);
                    int b = a == true ? 1 : 0;
                    tongTren = (double)tongTren + ((double)khachHangLoc[i].perSon * ((double)b - (double)khachHangLoc[i].trungBinh));
                    tongDuoi = (double)tongDuoi + (double)khachHangLoc[i].perSon;
                }
                item.pred = (double)trungBinhX + ((double)tongTren / (double)Math.Abs(tongDuoi));
                tongTren = 0;
                tongDuoi = 0;
            }

            var muaBanNguoi = db.muaban.Include(x => x.SANPHAM).Where(x => x.MAKH == ma).ToList();//Danh sách sản phẩm đã mua của người dùng hiện tại
                for (int j = 0; j < sanPhamLoc.Count(); j++)
                {
                    if (muaBanNguoi.Any(x=>x.SANPHAM.MODEL== sanPhamLoc[j].sanpham.MODEL))
                    {
                        sanPhamLoc.Remove(sanPhamLoc[j]);
                    j--;
                    }
                }

            sanPhamLoc = sanPhamLoc.OrderByDescending(x => x.pred).ToList();//Sắp xếp danh sách theo chỉ số Pred

            return sanPhamLoc.Take(8).ToList(); //Trả về 8 sản phẩm tiềm năng nhất
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static List<SanPhamLoc> TimTheoXem(string session)
        {
            //int ma = 50;
            List<string> SPDaXem1 = SplitString.CatChuoi(session).ToList();
            List<string> SPDaXem = SPDaXem1.Take(5).ToList();
            List<KhachHangLoc> KH = new List<KhachHangLoc>();
            List<SanPhamLoc> SP = new List<SanPhamLoc>();

            foreach(var item in SPDaXem)
            {
                var KHTam = db.Database.SqlQuery<int>("SP_LayKhachHang_Xem @MODEL", new SqlParameter("@MODEL", item)).ToList();
                foreach (var item2 in KHTam)
                {
                    KH.Add(new KhachHangLoc() { Ma = item2, perSon = 0, trungBinh = 0 });
                }
            }
            KH = KH.DistinctBy(x=>x.Ma).Take(10).ToList();

            foreach(var item in KH)
            {
                var SPTam = db.Database.SqlQuery<SanPhamStore>("SP_LaySanPham_Xem @MA", new SqlParameter("@MA", item.Ma)).ToList();
                foreach (var item2 in SPTam)
                {
                    SP.Add(new SanPhamLoc() { sanpham = item2, pred = 0 });
                }
            }
            SP = SP.DistinctBy(x=>x.sanpham.MODEL).ToList();

            int soKhachHang = KH.Count(); //Số khách hàng có tại hệ thống
            int soSanPham = SP.Count();//Số sản phẩm có tại hệ thống
            List<int> giaoDich = new List<int>(); //List chứa giao dịch của tất cả các người dùng, trừ người dùng hiện tại
            List<int> giaoDichNguoi = new List<int>();//List chỉ chứa giao dịch của người dùng hiện tại
            //int t = 0;
            var muaBan = db.muaban.Include(x => x.SANPHAM).ToList();
            //var muaBan = db.Database.SqlQuery<SuMuaBan>("SP_LayMuaBan @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            for (int i = 0; i < soKhachHang; i++)
            {
                for (int j = 0; j < soSanPham; j++)
                {
                    if (muaBan.Any(x => x.MAKH == KH[i].Ma && x.SANPHAM.MODEL == SP[j].sanpham.MODEL))
                    {
                        giaoDich.Add(1);
                    }
                    else
                    {
                        giaoDich.Add(0);
                    }
                }
            }

            foreach(var item in SP)
            {
                if (SPDaXem.Contains(item.sanpham.MODEL))
                {
                    giaoDichNguoi.Add(1);
                }
                else giaoDichNguoi.Add(0);
            }

            double trungBinhX = (double)0.0D;//Trung bình giao dịch người dùng hiện tại
            for (int i = 0; i < giaoDichNguoi.Count(); i++)//vòng for trungBinhX
            {
                trungBinhX = trungBinhX + (double)giaoDichNguoi[i] / soSanPham;
            }
            List<double> trungBinhY = new List<double>();//Danh sách trung bình giao dịch của mỗi người dùng
            double trungBinhTam = (double)0.0D;//biến tạm
            int dem = 1;
            for (int i = 0; i < giaoDich.Count(); i++)//Vòng for trungBinhY
            {
                trungBinhTam = trungBinhTam + (double)giaoDich[i] / (double)soSanPham;
                if (i == (soSanPham * dem) - 1)//kiểm tra vị trí kết thúc giao dịch của mỗi người dùng trong mảng
                {
                    trungBinhY.Add(trungBinhTam);
                    KH[dem - 1].trungBinh = trungBinhTam;
                    trungBinhTam = (double)0.0D;
                    dem++;
                }
            }
            double tongTren = (double)0.0D; //Phần trên của công thức
            double tongDuoiX = (double)0.0D; // Phần dưới của công thức theo X
            double tongDuoiY = (double)0.0D;//Phần dưới của công thức theo Y
            double tongDuoi = (double)0.0D;//phần dưới của công thức tongDuoi = tongDuoiX+tongDuoiY
            dem = 1;// hệ số nhân xác định vị trí dừng trong mãng giaoDich
            int dem2 = 0;
            int dem3 = 0;
            for (int i = 0; i < giaoDich.Count(); i++)//chạy tất cả các giaoDich, tính tổng trên và tổng dưới để ra pearson
            {
                tongTren = tongTren + ((double)giaoDichNguoi[dem3] - trungBinhX) * ((double)giaoDich[i] - (double)trungBinhY[dem2]);
                tongDuoiX = tongDuoiX + Math.Pow(((double)giaoDichNguoi[dem3] - (double)trungBinhX), 2);
                tongDuoiY = tongDuoiY + Math.Pow(((double)giaoDich[i] - (double)trungBinhY[dem2]), 2);
                dem3++;
                if (i == (soSanPham * dem) - 1)//kiểm tra xem đến vị trí kết thúc giaoDich của người dùng
                {
                    tongDuoi = Math.Sqrt((double)tongDuoiX * (double)tongDuoiY);//tổng dưới tongDuoi = tongDuoiX+tongDuoiY
                    if (tongDuoi == 0)//Kiểm tra nếu tổng dưới bằng không thì person=0, trách trường hợp chia cho 0
                    {
                        KH[dem2].perSon = 0;
                    }
                    else
                    {
                        KH[dem2].perSon = ((double)tongTren / (double)(tongDuoi));
                    }
                    tongTren = (double)0.0D; // Reset lại các kết quả
                    tongDuoiX = (double)0.0D;
                    tongDuoiY = (double)0.0D;
                    tongDuoi = (double)0.0D;
                    dem++;
                    dem2++;
                    dem3 = 0;
                }
            }

            List<SanPhamLoc> sanPhamLoc = new List<SanPhamLoc>(); //danh sách các sản phẩm có khả năng cho việc gợi ý
            List<KhachHangLoc> khachHangLoc = new List<KhachHangLoc>();// danh sách khách hàng có pearson lớn hơn ngưỡng= 0.25
            for (int i = 0; i < KH.Count(); i++)
            {
                if (KH[i].perSon >= 0.25)
                {
                    khachHangLoc.Add(KH[i]);
                }
            }

            foreach (var item1 in khachHangLoc)
            {
                var tam = db.Database.SqlQuery<SanPhamStore>("SP_LocSanPhamGoiY @ma", new SqlParameter("@ma", item1.Ma)).ToList();
                foreach (var item2 in tam)
                {
                    sanPhamLoc.Add(new SanPhamLoc { sanpham = item2, pred = 0 });
                }
            }

            tongTren = 0;
            tongDuoi = 0;
            bool a;
            foreach (var item in sanPhamLoc)
            {
                for (int i = 0; i < khachHangLoc.Count(); i++)
                {
                    a = muaBan.Any(x => x.MAKH == khachHangLoc[i].Ma && x.SANPHAM.MODEL == item.sanpham.MODEL);
                    int b = a == true ? 1 : 0;
                    tongTren = (double)tongTren + ((double)khachHangLoc[i].perSon * ((double)b - (double)khachHangLoc[i].trungBinh));
                    tongDuoi = (double)tongDuoi + (double)khachHangLoc[i].perSon;
                }
                item.pred = (double)trungBinhX + ((double)tongTren / (double)Math.Abs(tongDuoi));
                tongTren = 0;
                tongDuoi = 0;
            }

            for (int j = 0; j < sanPhamLoc.Count(); j++)
            {
                if (SPDaXem.Contains(sanPhamLoc[j].sanpham.MODEL))
                {
                    sanPhamLoc.Remove(sanPhamLoc[j]);
                    j--;
                }
            }

            sanPhamLoc = sanPhamLoc.OrderByDescending(x => x.pred).ToList();//Sắp xếp danh sách theo chỉ số Pred

            return sanPhamLoc.Take(8).ToList(); //Trả về 8 sản phẩm tiềm năng nhất
        }

        /// <summary>
        /// Đô tương tự giữa những sản phẩm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<SanPhamLoc> TimTheoSanPham(string model)
        {
            List<KhachHangLoc> KH = new List<KhachHangLoc>();
            List<SanPhamLoc> SP = new List<SanPhamLoc>();
            var KHTam = db.Database.SqlQuery<int>("SP_LayKhachHang_Model @MODEL", new SqlParameter("@MODEL", model)).ToList();
            foreach (var item in KHTam)
            {
                KH.Add(new KhachHangLoc() { Ma = item, perSon = 0, trungBinh = 0 });
            }
            var SPTam = db.Database.SqlQuery<SanPhamStore>("SP_LaySanPham_Model @MODEL", new SqlParameter("@MODEL", model)).ToList();
            foreach (var item in SPTam)
            {
                SP.Add(new SanPhamLoc() { sanpham = item, pred = 0 });
            }
            //var muaBan = db.muaban.Include(x => x.SANPHAM).ToList();
            var muaBan = db.Database.SqlQuery<SuMuaBan>("SP_LayMuaBan_Model @MODEL", new SqlParameter("@MODEL", model)).ToList();
            List<int> giaoDich = new List<int>();
            List<int> giaoDichHienTai =new List<int>();
            for(int i = 0; i < SP.Count; i++)
            {
                for(int j = 0; j < KH.Count; j++)
                {
                    if (muaBan.Any(x => x.MODEL == SP[i].sanpham.MODEL && x.MAKH == KH[j].Ma))
                    {
                        giaoDich.Add(1);
                        if (SP[i].sanpham.MODEL == model)
                        {
                            giaoDichHienTai.Add(1);
                        }
                    }
                    else
                    {
                        giaoDich.Add(0);
                        if (SP[i].sanpham.MODEL == model)
                        {
                            giaoDichHienTai.Add(0);
                        }
                    }
                }
            }

            double tongTren = 0.0D;
            double tongDuoiX = giaoDichHienTai.Sum();
            double tongDuoiY = 0.0D;
            int dem = 0;
            int dem1 = 1;
            for (int i = 0; i < giaoDich.Count; i++)
            {
                tongTren = tongTren + giaoDich[i] * giaoDichHienTai[dem];
                tongDuoiY = tongDuoiY + giaoDich[i];
                dem++;
                if(i == (KH.Count * dem1) - 1)
                {
                    SP[dem1 - 1].pred = (double)tongTren / ((double)Math.Sqrt(tongDuoiX) + (double)Math.Sqrt(tongDuoiY));
                    tongTren = 0.0D;
                    tongDuoiY = 0.0D;
                    dem = 0;
                    dem1++;
                }
            }
            List<SanPhamLoc> sanPhamLoc = new List<SanPhamLoc>();
            for(int i = 0; i < SP.Count; i++)
            {
                if (SP[i].pred > 0.25 && SP[i].sanpham.MODEL != model)
                {
                    sanPhamLoc.Add(SP[i]);
                }
            }
            sanPhamLoc = sanPhamLoc.OrderByDescending(x => x.pred).ToList();

            return sanPhamLoc.Take(8).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ma"></param>
        /// <returns></returns>
        public static List<SanPhamLoc> Given(int ma)
        {
            //int ma = 50;
            List<KhachHangLoc> KH = new List<KhachHangLoc>();
            List<SanPhamLoc> SP = new List<SanPhamLoc>();
            var KHTam = db.Database.SqlQuery<int>("SP_GIVEN_KhachHang @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            foreach (var item in KHTam)
            {
                KH.Add(new KhachHangLoc() { Ma = item, perSon = 0, trungBinh = 0 });
            }
            KH.Add(new KhachHangLoc() { Ma = ma, perSon = 0, trungBinh = 0 });
            var SPTam = db.Database.SqlQuery<SanPhamStore>("SP_GIVEN_SanPham @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            foreach (var item in SPTam)
            {
                SP.Add(new SanPhamLoc() { sanpham = item, pred = 0 });
            }

            List<string> model= db.Database.SqlQuery<string>("SP_LaySanPhamDau @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            int soKhachHang = KH.Count(); //Số khách hàng có tại hệ thống
            int soSanPham = SP.Count();//Số sản phẩm có tại hệ thống
            List<int> giaoDich = new List<int>(); //List chứa giao dịch của tất cả các người dùng, trừ người dùng hiện tại
            List<int> giaoDichNguoi = new List<int>();//List chỉ chứa giao dịch của người dùng hiện tại
            //int t = 0;
            //var muaBan = db.muaban.Include(x => x.SANPHAM).ToList();
            var muaBan = db.Database.SqlQuery<SuMuaBan>("SP_GIVEN_LayMuaBan @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            for (int i = 0; i < soKhachHang; i++)
            {
                for (int j = 0; j < soSanPham; j++)
                {
                    if (muaBan.Any(x => x.MAKH == KH[i].Ma && x.MODEL == SP[j].sanpham.MODEL))
                    {
                        if (KH[i].Ma != ma)
                           giaoDich.Add(1);
                        if (KH[i].Ma == ma)
                        {
                            if (SP[j].sanpham.MODEL == model[0])
                            {
                                giaoDich.Add(1);
                                giaoDichNguoi.Add(1);
                            }
                            else
                            {
                                giaoDichNguoi.Add(0);
                                giaoDich.Add(0);
                            }
                        }
                    }
                    else
                    {
                        giaoDich.Add(0);
                        if (KH[i].Ma == ma)
                        {                   
                            if (SP[j].sanpham.MODEL == model[0])
                            {
                                giaoDichNguoi.Add(1);
                            }
                            else giaoDichNguoi.Add(0);
                        }
                    }

                }
            }
            double trungBinhX = (double)0.0D;//Trung bình giao dịch người dùng hiện tại
            for (int i = 0; i < giaoDichNguoi.Count(); i++)//vòng for trungBinhX
            {
                trungBinhX = trungBinhX + (double)giaoDichNguoi[i] / soSanPham;
            }
            List<double> trungBinhY = new List<double>();//Danh sách trung bình giao dịch của mỗi người dùng
            double trungBinhTam = (double)0.0D;//biến tạm
            int dem = 1;
            for (int i = 0; i < giaoDich.Count(); i++)//Vòng for trungBinhY
            {
                trungBinhTam = trungBinhTam + (double)giaoDich[i] / (double)soSanPham;
                if (i == (soSanPham * dem) - 1)//kiểm tra vị trí kết thúc giao dịch của mỗi người dùng trong mảng
                {
                    trungBinhY.Add(trungBinhTam);
                    KH[dem - 1].trungBinh = trungBinhTam;
                    trungBinhTam = (double)0.0D;
                    dem++;
                }
            }
            double tongTren = (double)0.0D; //Phần trên của công thức
            double tongDuoiX = (double)0.0D; // Phần dưới của công thức theo X
            double tongDuoiY = (double)0.0D;//Phần dưới của công thức theo Y
            double tongDuoi = (double)0.0D;//phần dưới của công thức tongDuoi = tongDuoiX+tongDuoiY
            dem = 1;// hệ số nhân xác định vị trí dừng trong mãng giaoDich
            int dem2 = 0;
            int dem3 = 0;
            for (int i = 0; i < giaoDich.Count(); i++)//chạy tất cả các giaoDich, tính tổng trên và tổng dưới để ra pearson
            {
                tongTren = tongTren + ((double)giaoDichNguoi[dem3] - trungBinhX) * ((double)giaoDich[i] - (double)trungBinhY[dem2]);
                tongDuoiX = tongDuoiX + Math.Pow(((double)giaoDichNguoi[dem3] - (double)trungBinhX), 2);
                tongDuoiY = tongDuoiY + Math.Pow(((double)giaoDich[i] - (double)trungBinhY[dem2]), 2);
                dem3++;
                if (i == (soSanPham * dem) - 1)//kiểm tra xem đến vị trí kết thúc giaoDich của người dùng
                {
                    tongDuoi = Math.Sqrt((double)tongDuoiX * (double)tongDuoiY);//tổng dưới tongDuoi = tongDuoiX+tongDuoiY
                    if (tongDuoi == 0)//Kiểm tra nếu tổng dưới bằng không thì person=0, trách trường hợp chia cho 0
                    {
                        KH[dem2].perSon = 0;
                    }
                    else
                    {
                        KH[dem2].perSon = ((double)tongTren / (double)(tongDuoi));
                    }
                    tongTren = (double)0.0D; // Reset lại các kết quả
                    tongDuoiX = (double)0.0D;
                    tongDuoiY = (double)0.0D;
                    tongDuoi = (double)0.0D;
                    dem++;
                    dem2++;
                    dem3 = 0;
                }
            }

            List<SanPhamLoc> sanPhamLoc = new List<SanPhamLoc>(); //danh sách các sản phẩm có khả năng cho việc gợi ý
            List<KhachHangLoc> khachHangLoc = new List<KhachHangLoc>();// danh sách khách hàng có pearson lớn hơn ngưỡng= 0.25
            for (int i = 0; i < KH.Count(); i++)
            {
                if (KH[i].perSon > 0.1 && KH[i].Ma != ma)
                {
                    khachHangLoc.Add(KH[i]);
                }
            }

            foreach (var item1 in khachHangLoc)
            {
                var tam = db.Database.SqlQuery<SanPhamStore>("SP_GIVEN_LocSanPhamGoiY @ma", new SqlParameter("@ma", item1.Ma)).ToList();
                foreach (var item2 in tam)
                {
                    sanPhamLoc.Add(new SanPhamLoc { sanpham = item2, pred = 0 });
                }
            }
            tongTren = 0;
             tongDuoi = 0;
            bool a;
            foreach (var item in sanPhamLoc)
            {
                for (int i = 0; i < khachHangLoc.Count(); i++)
                {
                    a = muaBan.Any(x => x.MAKH == khachHangLoc[i].Ma && x.MODEL == item.sanpham.MODEL);
                    int b = a == true ? 1 : 0;
                    tongTren = (double)tongTren + ((double)khachHangLoc[i].perSon * ((double)b - (double)khachHangLoc[i].trungBinh));
                    tongDuoi = (double)tongDuoi + (double)khachHangLoc[i].perSon;
                }
                item.pred = (double)trungBinhX + ((double)tongTren / (double)Math.Abs(tongDuoi));
                tongTren = 0;
                tongDuoi = 0;
            }

            //var muaBanNguoi = db.Database.SqlQuery<SanPhamStore>("SP_LocSanPhamGoiY @ma", new SqlParameter("@ma", ma)).ToList();//Danh sách sản phẩm đã mua của người dùng hiện tại
            for (int j = 0; j < sanPhamLoc.Count(); j++)
            {
                if ( model[0].ToString() == sanPhamLoc[j].sanpham.MODEL)
                {
                    sanPhamLoc.Remove(sanPhamLoc[j]);
                    j--;
                }
            }

            sanPhamLoc = sanPhamLoc.OrderByDescending(x => x.pred).ToList();//Sắp xếp danh sách theo chỉ số Pred

            return sanPhamLoc.Take(8).ToList(); //Trả về 8 sản phẩm tiềm năng nhất
        }

        /// <summary>
        /// 
        /// </summary>
        public static void KiemTra()
        {
            int a = 0;
            int demGiven = 0;
            List<int> tapTest = new List<int>();
            tapTest = db.Database.SqlQuery<int>("SP_LayTapTest").ToList();
            List<SanPhamLoc> SanPham = new List<SanPhamLoc>();
            foreach(var item in tapTest) 
            {
                SanPham = GoiY.Given(item).ToList();
                var sanPhamI = db.Database.SqlQuery<SanPhamStore>("SP_GIVEN_LocSanPhamGoiY @ma", new SqlParameter("@ma", item)).ToList();

                foreach (var item2 in SanPham)
                {
                    if (sanPhamI.Count > 1 || SanPham.Count >1)
                        a++;
                    if (sanPhamI.Any(x=>x.MODEL== item2.sanpham.MODEL))
                    {
                        demGiven++;
                        goto aa;
                    }
                }

            aa:;
            }


          double given = (double)demGiven / 3260;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ma"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static List<SanPhamLoc> GhepGoiY(int ma, string model)
        {
            List<SanPhamLoc> goiYTheoSanPham = GoiY.TimTheoSanPham(model).ToList();
            List<SanPhamLoc> goiYTheoNguoiDung = GoiY.TimTheoNguoiDung(ma).Take(3).ToList();
            goiYTheoSanPham = goiYTheoSanPham.Take(8-goiYTheoNguoiDung.Count()).ToList();
            foreach (var item in goiYTheoNguoiDung)
                goiYTheoSanPham.Add(item);
            return goiYTheoSanPham.ToList();
        }

    }
}