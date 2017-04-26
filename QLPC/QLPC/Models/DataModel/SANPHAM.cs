namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            BAOHANHs = new HashSet<BAOHANH>();
            COMMENTs = new HashSet<COMMENT>();
            GIOHANGs = new HashSet<GIOHANG>();
            LINHKIENs = new HashSet<LINHKIEN>();
            MUABANs = new HashSet<MUABAN>();
            RATINGs = new HashSet<RATING>();
            RATINGs1 = new HashSet<RATING>();
            VIEWs = new HashSet<VIEW>();
            VIEWs1 = new HashSet<VIEW>();
            KHACHHANGs = new HashSet<KHACHHANG>();
        }

        [Key]
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string SERIAL { get; set; }

        [Display(Name = "Mã hãng sản xuất")]
        public int MAHSX { get; set; }

        [Display(Name = "Mã nhà phân phối")]
        public int MANPP { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên máy")]
        public string TENMAY { get; set; }

        [StringLength(100)]
        [Display(Name = "Mã hiệu (model)")]
        public string MODEL { get; set; }

        [Display(Name = "Giá niêm yết")]
        public decimal? GIA { get; set; }

        [Display(Name = "Số lượng")]
        public int? SOLUONG { get; set; }

        [Display(Name = "Thời gian bảo hành")]
        public int? THOIHANBAOHANH { get; set; }

        [StringLength(100)]
        [Display(Name = "Hình sản phẩm")]
        public string IMAGE { get; set; }

        [Column(TypeName = "xml")]
        [Display(Name = "Ảnh khác")]
        public string MOREIMAGE { get; set; }

        [Display(Name = "Giá khuyến mãi")]
        public decimal? GIAKHUYENMAI { get; set; }

        [Display(Name = "Mô tả chi tiết")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string DETAIL { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày nhập hàng")]
        public DateTime? NGAYNHAP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAOHANH> BAOHANHs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GIOHANG> GIOHANGs { get; set; }

        public virtual HANGSX HANGSX { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LINHKIEN> LINHKIENs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUABAN> MUABANs { get; set; }

        public virtual NHAPHANPHOI NHAPHANPHOI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATING> RATINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATING> RATINGs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIEW> VIEWs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIEW> VIEWs1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHACHHANG> KHACHHANGs { get; set; }
    }
}
