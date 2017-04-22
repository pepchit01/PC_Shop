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
        public static int Tim()
        {

            KHACHHANG[] KH = db.khachhang.Take(200).ToArray<KHACHHANG>();
            SANPHAM[] SP = db.sanpham.Take(200).ToArray<SANPHAM>();
            int soKhachHang = KH.Count(); //Số khách hàng có tại hệ thống
            int soSanPham = SP.Count();//Số sản phẩm có tại hệ thống
            //int[] giaoDich= new int[soKhachHang* soSanPham];
            List<int> giaoDich = new List<int>(); //List chứa giao dịch của tất cả các người dùng, trừ người dùng hiện tại
            List<int> giaoDichNguoi = new List<int>();//List chỉ chứa giao dịch của người dùng hiện tại
            int a = 0;
            int t = 0;
            for (int i=0;i< soKhachHang; i++)
            {
                for (int j=0;j< soSanPham; j++)
                {
                    a = db.Database.SqlQuery<SanPhamStore>("SP_KiemTra @khachhang, @sanpham", new SqlParameter("@khachhang", KH[i].MAKH), new SqlParameter("@sanpham", SP[j].MODEL)).Count();
                    if (a > 0) t++;
                    if (i+1 == 173)//Nếu là người dùng hiện tại sẽ chuyển giao dịch vào List giaoDichNguoi
                    {
                        giaoDich.Add(a > 0 ? 1 : 0);
                        giaoDichNguoi.Add(a > 0 ? 1 : 0); //Các giao dịch của người dùng hiện tại với tất cả sản phẩm
                    }
                    else
                    {
                        giaoDich.Add(a > 0 ? 1 : 0); //Các giao dịch của các người dùng còn lại với mỗi sản phẩm
                    }                                 
                }
            }

            List<double> person = new List<double>();//Danh sách chỉ số person của người dùng hiện tại với tất cả người còn lại
            double trungBinhX = (double)0.0D;//Trung bình giao dịch người dùng hiện tại
            for(int i = 0; i < giaoDichNguoi.Count(); i++)//vòng for trungBinhX
            {
                trungBinhX = trungBinhX + (double)giaoDichNguoi[i] / soSanPham;
            }
            List<double> trungBinhY = new List<double>();//Danh sách trung bình giao dịch của mỗi người dùng
            double trungBinhTam = (double)0.0D;//biến tạm
            int dem = 1;
            for (int i=0; i<giaoDich.Count(); i++)//Vòng for trungBinhY
            {
                trungBinhTam = trungBinhTam + (double)giaoDich[i] / (double)soSanPham;
                if (i == (soSanPham * dem)-1)//kiểm tra vị trí kết thúc giao dịch của mỗi người dùng trong mảng
                {
                    trungBinhY.Add(trungBinhTam);
                    trungBinhTam = (double)0.0D;
                    dem++;
                }             
            }
            double tongTren = (double)0.0D;
            double tongDuoiX = (double)0.0D;
            double tongDuoiY = (double)0.0D;
            double tongDuoi = (double)0.0D;
            dem = 1;
            int dem2 = 0;
            int dem3 = 0;
            for (int i = 0;i < giaoDich.Count(); i++)
            {
                    tongTren = tongTren + ((double)giaoDichNguoi[dem3] - trungBinhX) * ((double)giaoDich[i] - (double)trungBinhY[dem2]);
                    tongDuoiX = tongDuoiX + Math.Pow(((double)giaoDichNguoi[dem3] - (double)trungBinhX), 2);
                    tongDuoiY = tongDuoiY + Math.Pow(((double)giaoDich[i] - (double)trungBinhY[dem2]), 2);
                    dem3++;             
                if (i == (soSanPham * dem) - 1)
                {
                    tongDuoi = Math.Sqrt((double)tongDuoiX * (double)tongDuoiY);
                    if (tongDuoi == 0)
                    {
                        person.Add(0);
                    }
                    else
                    {
                        person.Add(((double)tongTren / (double)tongDuoi));
                    }
                    tongTren = (double)0.0D;
                    tongDuoiX = (double)0.0D;
                    tongDuoiY = (double)0.0D;
                    tongDuoi = (double)0.0D;
                    dem++;
                    dem2++;
                    dem3 = 0;
                }
            }

            List<int> maKhachHang = new List<int>();
            List<string> tenSanPham = new List<string>();
            List<double> personLoc = new List<double>();
            List<double> trunbinhYLoc = new List<double>();
            List<SanPhamStore> sanPhamLoc = new List<SanPhamStore>();
            for(int i = 0; i < person.Count(); i++)
            {
                if (person[i] >= 0.25)
                {
                    maKhachHang.Add(i+1);
                    personLoc.Add(person[i]);
                    trunbinhYLoc.Add(trungBinhY[i]);
                }
            }
            maKhachHang.Remove(173);
            foreach(var item1 in maKhachHang)
            {
                var tam = db.Database.SqlQuery<SanPhamStore>("SP_LocSanPhamGoiY @ma", new SqlParameter("@ma", item1)).ToList();
                foreach(var item2 in tam)
                {
                    sanPhamLoc.Add(item2);
                }
            }
            




            return 1;
        }
    }
}