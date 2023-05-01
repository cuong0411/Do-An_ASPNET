using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Do_An.Models
{
    public class Product
    {
        [Key]
        [Display(Name = "Mã Sản Phẩm")]
        public int Id { get; set; }
        [Display(Name = "Tên Sản Phẩm")]
        public string Name { get; set; }
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }
        [Display(Name = "Giá")]
        public long Price { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        // Navigation Property
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
