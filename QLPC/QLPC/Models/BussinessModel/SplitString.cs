using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLPC.Models.BussinessModel
{
    public static class SplitString
    {
        public static List<string> CatChuoi(string chuoiSanPham)
        {
            
            List<string> dsSanPham = new List<string>();
            char[] mangSanPham = chuoiSanPham.ToCharArray();
            string tam="";
            for(int i=0; i < mangSanPham.Count(); i++)
            {
                if (String.Compare(mangSanPham[i].ToString(),";") == 0)
                {
                    dsSanPham.Add(tam);
                    tam = "";
                }
                else
                {
                    tam = tam + mangSanPham[i];
                }              
            }
            return dsSanPham;
        }
    }
}