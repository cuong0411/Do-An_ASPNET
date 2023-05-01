﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Do_An.Models.DTO
{
    public class EditProductDTO
    {
        [Display(Name = "Tên Sản Phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
        public string Name { get; set; }
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Mô tả không được bỏ trống")]
        public string Description { get; set; }
        [Display(Name = "Giá Tiền")]
        [Required(ErrorMessage = "Giá tiền không được bỏ trống")]
        public long Price { get; set; }
        public string Image { get; set; }
        [Display(Name = "Hình Ảnh")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Chọn Danh Mục Sản Phẩm")]
        [Required(ErrorMessage = "Danh mục sản phẩm không được bỏ trống")]
        public int CategoryId { get; set; }
    }
}
