using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLPC.Models.BussinessModel
{
    public class SanPhamStore
    {

        public string TENMAY { get; set; }

        public string MODEL { get; set; }

        public string IMAGE { get; set; }

        public decimal GIA { get; set; }
    }

    public class KhachHangLoc
    {
        public int Ma { get; set; }
        public double perSon { get; set; }
        public double trungBinh { get; set; }
    }

    public class SanPhamLoc
    {
        public SanPhamStore sanpham { get; set; }
        public double pred { get; set; }
    }

    public class SuMuaBan
    {
        public int MAKH { get; set; }
        public string MODEL { get; set; }
    }
}