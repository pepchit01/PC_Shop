namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            BAOHANHs = new HashSet<BAOHANH>();
        }

        [Key]
        [Display(Name = "ID")]
        public int MANV { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập đầy đủ họ tên!.")]
        [StringLength(50,ErrorMessage ="Tên có độ dài dưới 50 kí tự.")]
        [Display(Name = "Tên nhân viên")]
        public string TENNV { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập số điện thoại!.")]
        [StringLength(12,ErrorMessage ="Hãy nhập đúng số điện thoại",MinimumLength =10)]
        [Display(Name = "Điện thoại")]
        public string DTHOAINV { get; set; }

        [Required(ErrorMessage ="Vùi lòng nhập địa chỉ!.")]
        [StringLength(1000)]
        [Display(Name = "Địa chỉ")]
        public string DCHINV { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        [StringLength(15)]
        [Display(Name = "Mật khẩu")]
        //[DataType(DataType.Password)]
        public string PASS { get; set; }

        [Required(ErrorMessage ="Vui lòng chọn quyền cho người dùng!.")]
        [Display(Name ="Quyền")]
        public int GRANT { get; set; }

        [StringLength(100)]
        [Display(Name = "Email nhân viên")]
        [DataType(DataType.EmailAddress)]
        public string EMAIL { get; set; }

        [StringLength(1000)]
        [Display(Name = "Ảnh đại diện")]
        public string AVATAR { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAOHANH> BAOHANHs { get; set; }
    }
}
