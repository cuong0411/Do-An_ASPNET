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
        Task<Category> UpdateAsync(int id, Category newCategory);
        Task DeleteAsync(int id);
    }
}
