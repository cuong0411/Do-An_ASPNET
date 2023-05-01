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
        public async Task Add(Category category)
        {
            await appDbContext.Categories.AddAsync(category);
            await appDbContext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetAll()
        {
            return await appDbContext.Categories.ToListAsync();
        }

        public Category GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Category Update(int id, Category newCategory)
        {
            throw new System.NotImplementedException();
        }
    }
}
