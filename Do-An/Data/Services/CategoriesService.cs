using Do_An.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Data.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext appDbContext;

        public CategoriesService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task AddAsync(Category category)
        {
            await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            appDbContext.Categories.Remove(category);
            await appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await appDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null)
            {
                return null;
            }
            return category;
        }

        public async Task<Category> UpdateAsync(int id, Category newCategory)
        {
            // Tìm danh mục với id được cung cấp
            var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null)
            {
                return null;
            }

            // Nếu tìm thấy thì cập nhật
            category.Name = newCategory.Name;
            await appDbContext.SaveChangesAsync();

            return category;
        }
    }
}
