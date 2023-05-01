using Do_An.Models;
using System.Collections.Generic;

namespace Do_An.Data.Services
{
    public interface ICategoriesService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        void Add(Category category);
        Category Update(int id, Category newCategory);
        void Delete(int id);
    }
}
