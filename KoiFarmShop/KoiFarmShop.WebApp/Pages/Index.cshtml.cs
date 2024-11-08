using KoiFarmShop.Repository;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.WebApp.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IKoiFishRepository koiFishRepository;
        private readonly KoiFarmShopContext _koiFarmShopContext;

        private const string CartSessionKey = "Cart";

        Cart cart = new Cart();

        public IndexModel(ILogger<IndexModel> logger, IKoiFishRepository koiFishRepository, KoiFarmShopContext koiFarmShopContext)
        {
            _logger = logger;
            this.koiFishRepository = koiFishRepository;
            _koiFarmShopContext = koiFarmShopContext;
        }

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

        public async Task<IActionResult> OnPostAddToCart(long koiFishId)
        {
            // Lay ca Koi tu database len
            var currrentKoiFish = await _koiFarmShopContext.KoiFishes
                       .Where(k => k.KoiFishId == koiFishId) // Replace 'yourKoiFishId' with the specific ID you're searching for
    .Select(k => new { k.KoiFishId, k.Price })
    .FirstOrDefaultAsync();

            CartItem cartItem = new CartItem();
            cartItem.KoiId = currrentKoiFish.KoiFishId;
            cartItem.Quantity = 1;
            cartItem.price = currrentKoiFish.Price;

            //Them ca Koi vao gio hang
            cart.AddKoi(cartItem);

            //Luu gio hang vao session
            HttpContext.Session.SetObject(CartSessionKey, cart);
            return RedirectToPage("/Customers/ViewCart");
        }
    }
}
