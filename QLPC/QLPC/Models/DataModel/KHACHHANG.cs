namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            COMMENTs = new HashSet<COMMENT>();
            MUABANs = new HashSet<MUABAN>();
            RATINGs = new HashSet<RATING>();
            VIEWs = new HashSet<VIEW>();
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        [StringLength(10)]
        [Display(Name = "ID")]
        public string MAKH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đầy đủ tên khách hàng")]
        [StringLength(200, ErrorMessage = "Độ dài tối đa là 200 kí tự")]
        [Display(Name = "Tên khách hàng")]
        public string TENKH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ khách hàng")]
        [StringLength(1000)]
        [Display(Name = "Địa chỉ khách hàng")]
        public string DCHIKH { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đúng số điện thoại")]
        [Display(Name = "Điện thoại")]
        [StringLength(12, ErrorMessage = "Số điện thoại không đúng", MinimumLength = 10)]
        public string DTHOAIKH { get; set; }

        [StringLength(50)]
        [Display(Name = "Tỉnh")]
        public string TINH { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [StringLength(15, ErrorMessage = "Độ dài mật khẩu từ 6-15 kí tự", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        public string PASS { get; set; }

        [StringLength(100,ErrorMessage ="Độ dài email dưới 100 kí tự.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Hãy nhập đúng Email của bạn")]
        public string EMAIL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENT> COMMENTs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MUABAN> MUABANs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATING> RATINGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VIEW> VIEWs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
