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
        [Key]
        public int MALOAI { get; set; }

        [Required]
        [StringLength(50)]
        public string TENLOAI { get; set; }
    }
}
