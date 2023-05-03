using System.ComponentModel.DataAnnotations;

namespace Do_An.Models.DTO
{
    public class Register
    {
        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "FullName is required")]
        public string FullName { get; set; }

        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Nhập lại Mật khẩu")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
