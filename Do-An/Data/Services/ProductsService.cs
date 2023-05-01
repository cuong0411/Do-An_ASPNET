using Do_An.Data.Base;
using Do_An.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Do_An.Data.Services
{
    public class ProductsService : EntityBaseRepository<Product>, IProductsService
    {
        private readonly AppDbContext appDbContext;

        public ProductsService(AppDbContext appDbContext) : base(appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            Product product = await appDbContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
            return product;
        }
    }
}
