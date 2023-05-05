using Do_An.Data.Static;
using Do_An.Models;
using Do_An.Models.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Data
{
    public class AppDbInitializer
    {
        /// <summary>
        /// Khởi tạo vài dữ liệu Category và Product
        /// nếu trong database không tồn tại bất cứ dữ liệu nào
        /// của 2 model trên
        /// </summary>
        /// <param name="applicationBuilder"></param>
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

        /// <summary>
        /// Khởi tạo 2 role Admin và User nếu không tồn tại.
        /// Khởi tạo 2 user admin-user và app-user tương ứng với 2 role có sẵn nếu không tồn tại.
        /// Hàm được gọi tại Startup file để kiểm tra mỗi khi chương trình được chạy
        /// </summary>
        /// <param name="applicationBuilder"></param>
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                // role section
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // nếu Admin role không tồn tại trong datatbase thì tạo mới
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                // nếu User role không tồn tại trong datatbase thì tạo mới
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                // user section
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@gmail.com";
                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Aa0000@@");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@gmail.com";
                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Aa0000@@");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
