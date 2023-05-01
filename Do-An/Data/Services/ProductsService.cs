using Do_An.Data.Base;
using Do_An.Models;

namespace Do_An.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        public ProductsService(AppDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
