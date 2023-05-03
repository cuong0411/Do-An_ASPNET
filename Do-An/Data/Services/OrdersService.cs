using Do_An.Data.Static;
using Do_An.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext appDbContext;

        public OrdersService(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders =  await appDbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(o => o.Product)
                .Include(o => o.User)
                .ToListAsync();

            if (userRole != UserRoles.Admin)
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }

            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmail)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmail
            };

            await appDbContext.Orders.AddAsync(order);
            await appDbContext.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    ProductId = item.Product.Id,
                    OrderId = order.Id,
                    Price = item.Product.Price
                };
                await appDbContext.OrderItems.AddAsync(orderItem);
            }
            await appDbContext.SaveChangesAsync();
        }
    }
}
