namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHAPHANPHOI")]
    public partial class NHAPHANPHOI
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAPHANPHOI()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [Display(Name = "Mã nhà phân phối")]
        public int MANPP { get; set; }

        [StringLength(200)]
        [Required(ErrorMessage = "Vui lòng nhập tên nhà phân phối")]
        [Display(Name = "Tên nhà phân phối")]
        public string TENNPP { get; set; }

        [StringLength(1000)]
        [Display(Name = "Địa chỉ nhà phân phối")]
        public string DCHINPP { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Vui lòng nhập điện thoại nhà phân phối")]
        [Display(Name = "Điện thoại")]
        public string DTHOAINPP { get; set; }

        [StringLength(12)]
        [Display(Name = "Fax")]
        public string FAXNPP { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
