using Do_An.Data;
using Do_An.Data.Services;
using Do_An.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }
        // GET: /Categories
        public async Task<IActionResult> Index()
        {
            var categories = await categoriesService.GetAllAsync();
            return View(categories);
        }

        // GET: /Categories/Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await categoriesService.AddAsync(category);

            // Trang danh sách category
            return RedirectToAction(nameof(Index));
        }

        // GET: Categories/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var category = await categoriesService.GetByIdAsync(id);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
        }
        // GET: /Categories/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoriesService.GetByIdAsync(id);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            await categoriesService.UpdateAsync(id, category);

            // Trang danh sách category
            return RedirectToAction(nameof(Index));
        }

        // GET: /Categories/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var category = await categoriesService.GetByIdAsync(id);
            if (category == null)
            {
                return View("NotFound");
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await categoriesService.GetByIdAsync(id);
            if (category == null)
            {
                return View("NotFound");
            }
            await categoriesService.DeleteAsync(id);

            // Trang danh sách category
            return RedirectToAction(nameof(Index));
        }
    }
}
