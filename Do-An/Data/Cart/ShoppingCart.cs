﻿using Do_An.Migrations;
using Do_An.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Do_An.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext appDbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public ShoppingCart(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider service)
        {
            // Session là cơ chế để lưu lại dữ liệu của phiên làm việc cho ứng dụng
            // ứng với từng khách truy cập. Để trao đổi dữ liệu từ trang này qua trang khác.
            // Ví dụ nếu người dùng đã đăng nhập, thì thông tin đăng nhập được lưu lại
            // và chuyển cho các trang khác nhau trong phiên làm việc
            // để mỗi lần gửi request lại thì khỏi phải đăng nhập,
            // hay người dùng chọn đựa mặt hàng vào giỏ hàng
            // thì phải nhớ khi chuyển đến trang thanh toán
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = service.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }
        public void AddItemToCart(Product product)
        {
            var shoppingCartItem = appDbContext.ShoppingCartItems
                .FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                // create new shoppingCartItem
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                appDbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            appDbContext.SaveChanges();
        }
        public void RemoveItemFromCart(Product product)
        {
            var shoppingCartItem = appDbContext.ShoppingCartItems
                .FirstOrDefault(n => n.Product.Id == product.Id && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    appDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            appDbContext.SaveChanges();
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = appDbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Include(s => s.Product).ToList());
        }
        public double GetShoppingCartTotal()
        {
            return appDbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .Select(n => n.Product.Price * n.Amount).Sum();
        }
        public async Task ClearShoppingCartAsync()
        {
            var items = await appDbContext.ShoppingCartItems
                .Where(s => s.ShoppingCartId == ShoppingCartId)
                .ToListAsync();
            appDbContext.ShoppingCartItems.RemoveRange(items);
            await appDbContext.SaveChangesAsync();

            ShoppingCartItems = new List<ShoppingCartItem>();
        }
    }
}
