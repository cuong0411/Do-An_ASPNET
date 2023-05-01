using Do_An.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly AppDbContext appDbContext;

        public CategoriesController(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await appDbContext.Categories.ToListAsync();
            return View(categories);
        }
    }
}
