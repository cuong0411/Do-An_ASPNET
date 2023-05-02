using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Do_An.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Họ Tên")]
        public string FullName { get; set; }
    }
}
