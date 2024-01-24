using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeOnline.Models
{
    public class KhachHangDangKyNhanh
    {

        [Display(Name = "Email:")]
        [Required(ErrorMessage = "Chưa nhập tài khoản")]
        public string Email { get; set; }

        [Key]
        [Display(Name = "Tài khoản:")]
        [Required(ErrorMessage = "Chưa nhập tài khoản")]
        public string TenDangNhap { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string MatKhau { get; set; }


        public bool NhoTaiKhoan { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string HoTenKH { get; set; }
    }
}