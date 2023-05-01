using Do_An.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Do_An.Data.Services
{
    public interface ICategoriesService
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetByIdAsync(int id);
        Task AddAsync(Category category);
        Category Update(int id, Category newCategory);
        void Delete(int id);
    }
}
