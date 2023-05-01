using Do_An.Data;
using Do_An.Data.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
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
        public IActionResult Create()
        {
            ViewData["Welcome"] = "Welcome to our store";
            ViewBag.Description = "This is a store description";
            return View();
        }
    }
}
