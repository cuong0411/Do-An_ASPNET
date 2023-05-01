using Do_An.Data.Base;
using Do_An.Models;
using System.Threading.Tasks;

namespace Do_An.Data.Services
{
    public interface IProductsService : IEntityBaseRepository<Product>
    {
        Task<Product> GetProductByIdAsync(int id);
    }
}
