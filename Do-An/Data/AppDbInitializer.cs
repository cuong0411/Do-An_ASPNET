using Do_An.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Do_An.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                // Category
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category() { Name = "CÀ PHÊ" },
                        new Category() { Name = "FREEZE" },
                        new Category() { Name = "TRÀ" },
                        new Category() { Name = "Bánh" }
                    });
                }
                context.SaveChanges();

                // Product
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product() 
                        { 
                            Name = "PhinDi Kem Sữa", 
                            Description="PhinDi Kem Sữa - Cà phê Phin thế hệ mới với chất Phin êm hơn, kết hợp cùng Kem Sữa béo ngậy mang đến hương vị mới lạ, không thể hấp dẫn hơn!", 
                            Price=45000, 
                            Image = "HLC_New_logo_5.1_Products__PHINDI_KEM_SUA.jpg", 
                            CategoryId = 1
                        },
                        new Product()
                        {
                            Name = "Freeze Trà Xanh",
                            Description="Thức uống rất được ưa chuộng! Trà xanh thượng hạng từ cao nguyên Việt Nam, kết hợp cùng đá xay, thạch trà dai dai, thơm ngon và một lớp kem dày phủ lên trên vô cùng hấp dẫn. Freeze Trà Xanh thơm ngon, mát lạnh, chinh phục bất cứ ai!",
                            Price=55000,
                            Image = "HLC_New_logo_5.1_Products__FREEZE_TRA_XANH.jpg",
                            CategoryId = 2
                        },
                        new Product()
                        {
                            Name = "Trà Sen Vàng (Củ Năng)",
                            Description="Thức uống chinh phục những thực khách khó tính! Sự kết hợp độc đáo giữa trà Ô long, hạt sen thơm bùi và củ năng giòn tan. Thêm vào chút sữa sẽ để vị thêm ngọt ngào.",
                            Price=45000,
                            Image = "HLC_New_logo_5.1_Products__TRA_SEN_VANG_CU_NANG.jpg",
                            CategoryId = 3
                        }
                    });
                }
                context.SaveChanges();
            }
        }
    }
}
