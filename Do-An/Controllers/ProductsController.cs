using Do_An.Data;
using Do_An.Data.Services;
using Do_An.Models;
using Do_An.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly IWebHostEnvironment host;
        private readonly ICategoriesService categoriesService;

        public ProductsController(IProductsService productsService, IWebHostEnvironment host, ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.host = host;
            this.categoriesService = categoriesService;
        }
        public async Task<IActionResult> Index()
        {
            // include Category property
            var products = await productsService.GetAllAsync(p => p.Category);
            return View(products);
        }

        // GET: /Products/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var product = await productsService.GetProductByIdAsync(id);
            return View(product);
        }

        // GET: /Products/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["Welcome"] = "Welcome to our store";
            //ViewBag.Description = "This is a store description";
            var categories = await categoriesService.GetAllAsync();
            ViewBag.categories = new SelectList(categories, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProductDTO addProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(addProductDTO);
            }
            
            // Save image to webrootpath
            var fileName = addProductDTO.ImageFile.FileName;
            var imagesFolder = Path.Combine(host.WebRootPath, "images");
            var filePath = Path.Combine(imagesFolder, fileName);
            addProductDTO.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));

            // Save image path in database
            addProductDTO.Image = fileName;

            // DTO => Domain
            Product product = new()
            {
                Name = addProductDTO.Name,
                Description = addProductDTO.Description,
                Price = addProductDTO.Price,
                Image = addProductDTO.Image,
                CategoryId = addProductDTO.CategoryId,
            };
            await productsService.AddAsync(product);

            return RedirectToAction(nameof(Index));
        }

        // GET: /Products/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var categories = await categoriesService.GetAllAsync();
            ViewBag.categories = new SelectList(categories, "Id", "Name");

            var product = await productsService.GetByIdAsync(id);
            if (product == null)
            {
                return View("NotFound");
            }

            // Domain => DTO
            var eidtProductDTO = new EditProductDTO()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                CategoryId = product.CategoryId,
            };
            return View(eidtProductDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditProductDTO editProductDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(editProductDTO);
            }

            // Domain
            var product = await productsService.GetProductByIdAsync(id);
            if (product == null)
            {
                return View("NotFound");
            }

            // Khi update, nếu người dùng upload ảnh mới
            // thì xóa ảnh cũ trong webrootpath
            // cập nhật tên file và copy ảnh mới vào webrootpath
            if (editProductDTO.ImageFile != null)
            {
                var fileName = product.Image; // image name in images folder
                var imagesFolder = Path.Combine(host.WebRootPath, "images");
                var filePath = Path.Combine(imagesFolder, fileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath); // Delete old image
                }

                // Save new image to webrootpath
                var newFileName = editProductDTO.ImageFile.FileName;
                var newFilePath = Path.Combine(imagesFolder, newFileName);
                editProductDTO.ImageFile.CopyTo(new FileStream(newFilePath, FileMode.Create));

                // Save new image path in database
                product.Image = newFileName;
            }


            // DTO => Domain

            product.Name = editProductDTO.Name;
            product.Description = editProductDTO.Description;
            product.Price = editProductDTO.Price;
            product.CategoryId = editProductDTO.CategoryId;

            await productsService.UpdateAsync(id, product);
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var product = await productsService.GetProductByIdAsync(id);
            if (product == null) { return View("NotFound"); }
            return View(product);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await productsService.GetProductByIdAsync(id);
            if (product == null) { return View("NotFound"); }

            await productsService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
