using Do_An.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace Do_An.Models
{
    public class Category : IEntityBase
    {
        [Key]
        [Display(Name = "Mã Danh Mục")]
        public int Id { get; set; }
        [Display(Name = "Tên Danh Mục")]
        [Required(ErrorMessage = "Tên Danh Mục không được bỏ trống")]
        public string Name { get; set; }
    }
}
