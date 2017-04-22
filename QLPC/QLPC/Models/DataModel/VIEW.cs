namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VIEW")]
    public partial class VIEW
    {
        [Key]
        [Display(Name = "Mã lượt xem")]
        public long STTVIEW { get; set; }

        [Display(Name = "Mã khách hàng")]
        public int MAKH { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string SERIAL { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày xem")]
        public DateTime? NGAYXEM { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

        public virtual SANPHAM SANPHAM1 { get; set; }
    }
}
