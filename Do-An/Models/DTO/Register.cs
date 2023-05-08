using System.ComponentModel.DataAnnotations;

namespace Do_An.Models.DTO
{
    public class Register
    {
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Trường không được bỏ trống")]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Trường không được bỏ trống")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Trường không được bỏ trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại Mật khẩu")]
        [Required(ErrorMessage = "Trường không được bỏ trống")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
        public string ConfirmPassword { get; set; }
    }
}
