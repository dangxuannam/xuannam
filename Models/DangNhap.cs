using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CafeOnline.Models
{
    public class DangNhap
    {
        [Key]
        [Display(Name = "Tên đăng nhập/ Email:")]
        [Required(ErrorMessage = "Chưa nhập tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Mật khẩu:")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        public string Password { get; set; }

        [Display(Name = "Nhớ tài khoản")]
        public bool NhoTaiKhoan { get; set; }
    }
}