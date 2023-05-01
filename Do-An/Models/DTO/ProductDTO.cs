using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Do_An.Models.DTO
{
    public class ProductDTO
    {
        [Display(Name = "Product Name")]
        [Required(ErrorMessage = "Product Name is required")]
        public string Name { get; set; }
        [Display(Name = "Product Description")]
        [Required(ErrorMessage = "Product Description is required")]
        public string Description { get; set; }
        [Display(Name = "Product Price")]
        [Required(ErrorMessage = "Product Price is required")]
        public long Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Product ImageFile")]
        [Required(ErrorMessage = "Product ImageFile is required")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Select a Category")]
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
    }
}
