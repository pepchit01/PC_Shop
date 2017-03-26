namespace QLPC.Models.DataModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class QLPC : DbContext
    {
        public QLPC()
            : base("name=QLPC")
        {
        }

        public virtual DbSet<BAOHANH> BAOHANHs { get; set; }
        public virtual DbSet<COMMENT> COMMENTs { get; set; }
        public virtual DbSet<HANGSX> HANGSXes { get; set; }
        public virtual DbSet<KHACHHANG> KHACHHANGs { get; set; }
        public virtual DbSet<LINHKIEN> LINHKIENs { get; set; }
        public virtual DbSet<LOAILK> LOAILKs { get; set; }
        public virtual DbSet<MUABAN> MUABANs { get; set; }
        public virtual DbSet<NHANVIEN> NHANVIENs { get; set; }
        public virtual DbSet<NHAPHANPHOI> NHAPHANPHOIs { get; set; }
        public virtual DbSet<RATING> RATINGs { get; set; }
        public virtual DbSet<SANPHAM> SANPHAMs { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<VIEW> VIEWs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BAOHANH>()
                .Property(e => e.SERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<BAOHANH>()
                .Property(e => e.MAKH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<COMMENT>()
                .Property(e => e.SERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<COMMENT>()
                .Property(e => e.MAKH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<HANGSX>()
                .Property(e => e.DTHOAIHSX)
                .IsUnicode(false);

            modelBuilder.Entity<HANGSX>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.HANGSX)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.MAKH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.DTHOAIKH)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.PASS)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.COMMENTs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.MUABANs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.RATINGs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.VIEWs)
                .WithRequired(e => e.KHACHHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHACHHANG>()
                .HasMany(e => e.SANPHAMs)
                .WithMany(e => e.KHACHHANGs)
                .Map(m => m.ToTable("GIOHANG").MapLeftKey("MAKH").MapRightKey("SERIAL"));

            modelBuilder.Entity<LINHKIEN>()
                .Property(e => e.SERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<MUABAN>()
                .Property(e => e.SERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<MUABAN>()
                .Property(e => e.MAKH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<MUABAN>()
                .Property(e => e.SOTIEN)
                .HasPrecision(18, 0);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.DTHOAINV)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.PASS)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .Property(e => e.EMAIL)
                .IsUnicode(false);

            modelBuilder.Entity<NHANVIEN>()
                .HasMany(e => e.BAOHANHs)
                .WithOptional(e => e.NHANVIEN)
                .HasForeignKey(e => e.MANV_LAP);

            modelBuilder.Entity<NHAPHANPHOI>()
                .Property(e => e.DTHOAINPP)
                .IsUnicode(false);

            modelBuilder.Entity<NHAPHANPHOI>()
                .Property(e => e.FAXNPP)
                .IsUnicode(false);

            modelBuilder.Entity<NHAPHANPHOI>()
                .HasMany(e => e.SANPHAMs)
                .WithRequired(e => e.NHAPHANPHOI)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<RATING>()
                .Property(e => e.SERIAL)
                .IsUnicode(false);

            modelBuilder.Entity<RATING>()
                .Property(e => e.MAKH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                 .Property(e => e.SERIAL)
                 .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.MODEL)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.GIA)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.GIAKHUYENMAI)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SANPHAM>()
                .Property(e => e.DETAIL)
                .IsUnicode(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.COMMENTs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.MUABANs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.RATINGs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.LINHKIENs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SANPHAM>()
                .HasMany(e => e.VIEWs)
                .WithRequired(e => e.SANPHAM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<VIEW>()
                .Property(e => e.MAKH)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<VIEW>()
                .Property(e => e.SERIAL)
                .IsUnicode(false);
        }
    }
}
