using Do_An.Data.Cart;
using Do_An.Data.Services;
using Do_An.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Do_An.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ShoppingCart shoppingCart;
        private readonly IOrdersService ordersService;

        public OrdersController(IProductsService productsService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            this.productsService = productsService;
            this.shoppingCart = shoppingCart;
            this.ordersService = ordersService;
        }
        public IActionResult ShoppingCart()
        {
            var items = shoppingCart.GetShoppingCartItems();
            shoppingCart.ShoppingCartItems = items;
            var response = new ShoppingCartDTO()
            {
                ShoppingCart = shoppingCart,
                ShoppingCartToal = shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var product = await productsService.GetProductByIdAsync(id);

            if (product != null) 
            {
                shoppingCart.AddItemToCart(product);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var product = await productsService.GetProductByIdAsync(id);

            if (product != null)
            {
                shoppingCart.RemoveItemFromCart(product);
            }

            return RedirectToAction(nameof(ShoppingCart));
        }
        public async Task<IActionResult> CompleteOrder()
        {
            var items = shoppingCart.GetShoppingCartItems();
            string userId = "";
            string userEmail = "";

            await ordersService.StoreOrderAsync(items, userId, userEmail);
            await shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }
    }
}
