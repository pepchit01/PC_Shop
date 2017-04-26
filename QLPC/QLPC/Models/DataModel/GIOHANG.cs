namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GIOHANG")]
    public partial class GIOHANG
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MA { get; set; }

        public int MAKH { get; set; }

        [Required]
        [StringLength(50)]
        public string MODEL { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }

    }
}
