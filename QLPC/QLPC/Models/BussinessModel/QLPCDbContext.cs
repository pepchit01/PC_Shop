using QLPC.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Collections;

namespace QLPC.Models.BussinessModel
{
    public class QLPCDbContext:DbContext
    {
        public QLPCDbContext():base("name=QLPC")
        {
             //< add name = "QLPC" connectionString = "data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\QLPC.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;App=EntityFramework" providerName = "System.Data.SqlClient" />
        }

        public DbSet<BAOHANH> baohanh { get; set;}
        public DbSet<COMMENT> comment { get; set; }
        public DbSet<HANGSX>  hangsanxuat{ get; set; }
        public DbSet<KHACHHANG> khachhang { get; set; }
        public DbSet<LINHKIEN> linhkien { get; set; }
        public DbSet<LOAILK> loailk { get; set; }
        public DbSet<MUABAN> muaban { get; set; }
        public DbSet<NHANVIEN> nhanvien { get; set; }
        public DbSet<NHAPHANPHOI> nhaphanphoi { get; set; }
        public DbSet<RATING> rating { get; set; }
        public DbSet<SANPHAM> sanpham { get; set; }
        public DbSet<VIEW> view { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}


