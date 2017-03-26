namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HANGSX")]
    public partial class HANGSX
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HANGSX()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [Display(Name = "ID")]
        public int MAHSX { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập tên hãng")]
        [Display(Name = "Tên hãng sản xuất")]
        public string TENHSX { get; set; }

        [StringLength(1000)]
        [Display(Name = "Địa chỉ hãng sản xuất")]
        public string DCHIHSX { get; set; }

        [StringLength(12)]
        [Display(Name = "Điện thoại")]
        public string DTHOAIHSX { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
