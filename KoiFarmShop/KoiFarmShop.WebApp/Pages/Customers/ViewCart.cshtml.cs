using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Models.Items;
using KoiFarmShop.WebApp.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KoiFarmShop.WebApp.Pages.Customers
{
    public class ViewCartModel : PageModel
    {
        public Cart cart;
        private const string CartSessionKey = "Cart";
        public double? totalAmount { get; set; }

        public void OnGet()
        {
            if (HttpContext.Session != null)
            {
                cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
            }
            else
            {
                cart = new Cart();
            }
        }

        public IActionResult OnPostUpdateCart(long KoiFishId, int Quantity)
        {
            var cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();

            var koiFish = cart.Items.FirstOrDefault(item => item.KoiId == KoiFishId);
            if (koiFish != null && Quantity > 0)
            {
                koiFish.Quantity = Quantity;
            }
            else if (koiFish != null && Quantity == 0)
            {
                cart.RemoveKoi(cart.Items.FirstOrDefault(item => item.KoiId == KoiFishId));
            }
            SaveCart(cart);
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveItem(long KoiFishId)
        {
            var cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
            cart.RemoveKoi(cart.Items.FirstOrDefault(item => item.KoiId == KoiFishId));
            SaveCart(cart);
            return RedirectToPage();
        }

        private void SaveCart(Cart cartItems)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));
        }
    }
}
