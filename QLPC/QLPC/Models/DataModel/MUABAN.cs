namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MUABAN")]
    public partial class MUABAN
    {
        [Key]
        [Display(Name = "ID")]
        public long MAMB { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string SERIAL { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Mã khách hàng")]
        public string MAKH { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày mua")]
        [DataType(DataType.Date)]
        public DateTime? NGAYMUA { get; set; }

        [Display(Name = "Số tiền")]
        public decimal? SOTIEN { get; set; }

        [Display(Name = "Số lượng")]
        public int? SOLUONG { get; set; }

        [Display(Name = "Đã thanh toán")]
        public bool THANHTOAN { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
