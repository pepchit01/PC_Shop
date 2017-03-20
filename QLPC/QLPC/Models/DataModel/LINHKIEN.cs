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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LINHKIEN()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [Display(Name = "Mã linh kiện")]
        public int MALK { get; set; }

        [StringLength(200)]
        [Display(Name = "Tên kiện")]
        public string TENLK { get; set; }

        [Display(Name = "Mã loại")]
        public int? MALOAI { get; set; }

        public virtual LOAILK LOAILK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
