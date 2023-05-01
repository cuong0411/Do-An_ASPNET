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

        [System.Obsolete]
        public ProductsController(IProductsService productsService, IWebHostEnvironment host, ICategoriesService categoriesService)
        {
            this.productsService = productsService;
            this.host = host;
            this.categoriesService = categoriesService;
        }
        public async Task<IActionResult> Index()
        {
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
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(productDTO);
            }
            
            // Save image to webrootpath
            var fileName = productDTO.ImageFile.FileName;
            var imagesFolder = Path.Combine(host.WebRootPath, "images");
            var filePath = Path.Combine(imagesFolder, fileName);
            productDTO.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));

            // Save image path in database
            productDTO.Image = Path.Combine("images", fileName);

            // DTO => Domain
            Product product = new()
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Image = productDTO.Image,
                CategoryId = productDTO.CategoryId,
            };
            await productsService.AddAsync(product);

            return RedirectToAction(nameof(Index));
        }
    }
}
