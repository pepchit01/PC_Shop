namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LINHKIEN")]
    public partial class LINHKIEN
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public LINHKIEN()
        //{
        //    SANPHAM = new HashSet<SANPHAM>();
        //}

        [Key]
        [Display(Name = "Mã")]
        public long STT { get; set; }

        [StringLength(50)]
        [Display(Name = "Mã sản phẩm")]
        public string SERIAL { get; set; }

        [StringLength(200)]
        [Display(Name = "Tên linh kiện")]
        public string TENLK { get; set; }

        [Display(Name = "Loại linh kiện")]
        [StringLength(50)]
        public string TENLOAI { get; set; }


        //     [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual SANPHAM SANPHAM { get; set; }
    }
}
