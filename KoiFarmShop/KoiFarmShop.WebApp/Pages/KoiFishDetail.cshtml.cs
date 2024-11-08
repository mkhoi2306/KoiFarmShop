using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;
using KoiFarmShop.WebApp.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages
{
	public class KoiFishDetailModel : PageModel
	{
		private readonly IKoiFishService _koiFishService;

		Cart _cart = new Cart();
		
		[BindProperty]
		public KoiFish KoiFish { get; set; } = default!;

		public KoiFishDetailModel(IKoiFishService koiFishService)
		{
			this._koiFishService = koiFishService;
		}
        public async Task<IActionResult> OnGet(long id)  // Chắc chắn có tham số 'id'
        {
	        if (HttpContext.Session != null)
	        {
		        _cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();
	        }
	        else
	        {
		        _cart = new Cart();
	        }
            KoiFish = await _koiFishService.GetKoiFishByIdAsync(id);  // Sử dụng service để lấy dữ liệu cá koi
            if (KoiFish == null)
            {
                return NotFound();  // Nếu không tìm thấy cá koi, trả về lỗi 404
            }

            return Page();  // Trả về trang Razor với dữ liệu cá koi
        }

        public async Task<IActionResult> OnPost(long id)
        {

		        // Check if user is logged in
		        if (!User.Identity.IsAuthenticated)
		        {
			        TempData["Message"] = "Please log in to add items to your cart.";
			        return RedirectToPage("/Auth/Login");
		        }

		        // Retrieve the current cart from session
		        var cart = HttpContext.Session.GetObject<Cart>("Cart") ?? new Cart();

		        KoiFish = await _koiFishService.GetKoiFishByIdAsync(id);
            
		        CartItem cartItem = new CartItem();
		        cartItem.KoiId = KoiFish.KoiFishId;
		        cartItem.Quantity = 1;
		        cartItem.price = KoiFish.Price;
		        // Add the Koi Fish to the cart
		        cart.AddKoi(cartItem);

		        // Save the updated cart back to session
		        HttpContext.Session.SetObject("Cart", cart);

		        // Optionally redirect to a cart page or stay on the current page
		        TempData["Message"] = "Added to cart successfully!";
		        return RedirectToPage();  // Or redirect to another page like cart
	        

	        return Page();
        }
	}
}
