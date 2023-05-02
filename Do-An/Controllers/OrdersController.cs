using Do_An.Data.Cart;
using Do_An.Data.Services;
using Do_An.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Do_An.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ShoppingCart shoppingCart;

        public OrdersController(IProductsService productsService, ShoppingCart shoppingCart)
        {
            this.productsService = productsService;
            this.shoppingCart = shoppingCart;
        }
        public IActionResult Index()
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
    }
}
