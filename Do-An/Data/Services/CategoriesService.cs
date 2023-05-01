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

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await appDbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            Category category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public Category Update(int id, Category newCategory)
        {
            throw new System.NotImplementedException();
        }
    }
}
