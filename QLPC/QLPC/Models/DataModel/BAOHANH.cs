namespace QLPC.Models.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BAOHANH")]
    public partial class BAOHANH
    {
        [Key]
        [Display(Name = "Mã biên nhận")]
        public long STTBN { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập đúng mã sản phẩm")]
        [StringLength(50, ErrorMessage = "Độ dài Serial của sản phẩm nhỏ hơn 50 kí tự")]
        [Display(Name = "Mã Sản Phẩm")]
        public string SERIAL { get; set; }

        [Display(Name = "Lần bảo hành thứ")]
        public int? LANBAOHANH { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày nhận bảo hành")]
        [DataType(DataType.Date)]
        public DateTime? NGAYNHAN { get; set; }

        [Display(Name = "Nhân viên nhận máy")]
        public int? MANV_LAP { get; set; }

        [Display(Name = "Tình trạng máy")]
        [StringLength(1000, ErrorMessage = "Mô tả ngắn gọn, dưới 1000 kí tự")]
        public string TINHTRANG { get; set; }

        [Display(Name = "Lỗi phần cứng")]
        public byte? LOIPHANCUNG { get; set; }

        [Display(Name = "Công việc sửa chữa")]
        [StringLength(1000)]
        public string CONGVIEC { get; set; }

        [Display(Name = "Phụ kiện kèm theo máy")]
        [StringLength(1000)]
        public string PHUKIEN { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày trả máy")]
        [DataType(DataType.Date)]
        public DateTime? NGAYTRA { get; set; }

        [StringLength(10)]
        [Display(Name = "Người gửi máy")]
        public string MAKH { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
