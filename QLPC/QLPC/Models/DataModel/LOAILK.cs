namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LOAILK")]
    public partial class LOAILK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        [Key]
        public int MALOAI { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Tên Loại")]
        public string TENLOAI { get; set; }

    }
}
