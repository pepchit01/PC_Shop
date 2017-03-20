namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("COMMENT")]
    public partial class COMMENT
    {
        [Key]
        [Display(Name = "Mã comment")]
        public long STTCM { get; set; }

        [Required]
        [Display(Name = "Mã sản phẩm")]
        [StringLength(50)]
        public string SERIAL { get; set; }

        [Required]
        [Display(Name = "Mã khách hàng")]
        [StringLength(10)]
        public string MAKH { get; set; }


        [Column(TypeName = "date")]
        [Display(Name = "Ngày bình luận")]
        public DateTime? NGAY { get; set; }

        [Column("COMMENT")]
        [Display(Name = "Nội dung bình luận")]
        public string COMMENT1 { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
