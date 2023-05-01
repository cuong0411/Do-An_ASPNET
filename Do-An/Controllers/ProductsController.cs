using Do_An.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext appDbContext;

        public ProductsController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var products = await appDbContext.Products.Include(p => p.Category).ToListAsync();
            return View(products);
        }
    }
}
