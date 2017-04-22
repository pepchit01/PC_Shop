namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RATING")]
    public partial class RATING
    {
        [Key]
        [Display(Name = "Mã đánh giá")]
        public long STTDG { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string SERIAL { get; set; }

        [Required]
        [Display(Name = "Mã khách hàng")]
        public int MAKH { get; set; }

        [Display(Name = "Giá trị đánh giá")]
        public int GIATRI { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày đánh giá")]
        public DateTime? NGAY { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

        public virtual SANPHAM SANPHAM1 { get; set; }
    }
}
