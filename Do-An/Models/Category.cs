using System.ComponentModel.DataAnnotations;

namespace Do_An.Models
{
    public class Category
    {
        [Key]
        [Display(Name = "Mã Danh Mục")]
        public int Id { get; set; }
        [Display(Name = "Tên Danh Mục")]
        [Required(ErrorMessage = "Tên Danh Mục không được bỏ trống")]
        public string Name { get; set; }
    }
}
