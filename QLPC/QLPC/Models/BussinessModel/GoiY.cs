using System;
using System.Collections.Generic;
using System.Linq;
using QLPC.Models.DataModel;
using System.Data.SqlClient;
using System.Data.Entity;

namespace QLPC.Models.BussinessModel
{
    public class GoiY
    {
        public static QLPCDbContext db = new QLPCDbContext();
        public static List<SanPhamLoc> Tim(int ma)
        {
            //int ma = 50;
            List<KhachHangLoc> KH = new List<KhachHangLoc>();
            List<SanPhamLoc> SP = new List<SanPhamLoc>();
            var KHTam = db.Database.SqlQuery<int>("SP_LayKhachHang @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            //KH.Add(new KhachHangLoc() { Ma= ma, perSon=0,trungBinh=0});
                foreach(var item in KHTam)
                {
                    KH.Add(new KhachHangLoc() { Ma = item,perSon=0,trungBinh=0 });
                }
            var SPTam = db.Database.SqlQuery<SanPhamStore>("SP_LaySanPham @MAKH", new SqlParameter("@MAKH", ma)).ToList();
            foreach(var item in SPTam)
            {
                SP.Add(new SanPhamLoc() {sanpham=item,pred=0 });
            }

            //KHACHHANG[] KH = db.khachhang.ToArray<KHACHHANG>();
            //SANPHAM[] SP = db.sanpham.ToArray<SANPHAM>();
            int soKhachHang = KH.Count(); //Số khách hàng có tại hệ thống
            int soSanPham = SP.Count();//Số sản phẩm có tại hệ thống
            //int[] giaoDich= new int[soKhachHang* soSanPham];
            List<int> giaoDich = new List<int>(); //List chứa giao dịch của tất cả các người dùng, trừ người dùng hiện tại
            List<int> giaoDichNguoi = new List<int>();//List chỉ chứa giao dịch của người dùng hiện tại
            //int t = 0;
            var muaBan = db.muaban.Include(x => x.SANPHAM).ToList();
            for (int i = 0; i < soKhachHang; i++)
            {
                for (int j = 0; j < soSanPham; j++)
                {   
                    if (muaBan.Any(x => x.MAKH == KH[i].Ma && x.SANPHAM.MODEL == SP[j].sanpham.MODEL))
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
                       // person.Add(((double)tongTren / (double)tongDuoi)); //pearson cuối của mỗi người 
                        KH[dem2].perSon = ((double)tongTren / (double)tongDuoi);
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

            //foreach (var item in khachHangLoc)//loại bỏ khách hàng hiện tại khỏi danh sách các khách hàng dc chọn 
            //{
            //    if (item.Ma == ma)
            //    {
            //        khachHangLoc.Remove(item);
            //        break;
            //    }
            //}
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
                    //a = db.Database.SqlQuery<SanPhamStore>("SP_KiemTra @khachhang, @sanpham", new SqlParameter("@khachhang", khachHangLoc[i].Ma), new SqlParameter("@sanpham", item.sanpham.MODEL)).Count();
                    a = muaBan.Any(x => x.MAKH == khachHangLoc[i].Ma && x.SANPHAM.MODEL == item.sanpham.MODEL);
                    int b = a == true ? 1 : 0;
                    tongTren = (double)tongTren + ((double)khachHangLoc[i].perSon * ((double)b - (double)khachHangLoc[i].trungBinh));
                    tongDuoi = (double)tongDuoi + (double)khachHangLoc[i].perSon;
                }
                item.pred = (double)trungBinhX + ((double)tongTren / (double)tongDuoi);
                tongTren = 0;
                tongDuoi = 0;
            }

            for (int i = 0; i < sanPhamLoc.Count()-1; i++)
            {
                for (int j = i+1; j < sanPhamLoc.Count(); j++)
                {
                    if (sanPhamLoc[i].sanpham.MODEL == sanPhamLoc[j].sanpham.MODEL)
                    {
                        sanPhamLoc.Remove(sanPhamLoc[i]);
                    }
                }
            }
            sanPhamLoc.Take(5);




            return sanPhamLoc.Take(5).ToList();
        }
    }
}