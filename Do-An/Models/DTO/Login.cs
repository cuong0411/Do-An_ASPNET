using System.ComponentModel.DataAnnotations;

namespace Do_An.Models.DTO
{
    public class Login
    {
        [Display(Name = "Địa chỉ email")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
