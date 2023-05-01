using Do_An.Data.Base;
using Do_An.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Data.Services
{
    public class CategoriesService : EntityBaseRepository<Category>, ICategoriesService
    {
        public CategoriesService(AppDbContext appDbContext) : base(appDbContext) { }
    }
}
