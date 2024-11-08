using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KoiFarmShop.WebApp.Pages
{
    public class KoiFishDetailModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;

        public KoiFish KoiFish { get; set; } = new KoiFish();

        public KoiFishDetailModel(IKoiFishService koiFishService)
        {
            this._koiFishService = koiFishService;
        }
        public async Task<IActionResult> OnGet(long? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            KoiFish = /*await _koiFishService.GetKoiFishById2Async(id.Value);*/ null;
            if (KoiFish == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string handler = Request.Form["handler"];
            if (handler != null && handler == "AddToCart")
            {
                if (!User.Identity.IsAuthenticated)
                {
                    TempData["Message"] = "Please log in to add items to your cart.";
                    return RedirectToPage("/Auth/Login");
                }
                return Page();
            }

            return Page();
        }
    }
}
