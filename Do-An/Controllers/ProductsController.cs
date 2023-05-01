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
            var products = await productsService.GetAllAsync();
            return View(products);
        }
    }
}
