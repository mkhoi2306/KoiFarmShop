using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;
using Microsoft.AspNetCore.Authorization;

namespace KoiFarmShop.WebApp.Pages.Staff
{
	[Authorize(Roles = "Staff")]
	public class EditModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;

        public EditModel(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; }

        [BindProperty]
        public IFormFile KoiImage { get; set; }


        public async Task<IActionResult> OnGetAsync(long id)
        {
            KoiFish = await _koiFishService.GetKoiFishByIdAsync(id);
            if (KoiFish == null)
            {
                return RedirectToPage("/Staff/Index");
            }

            if (string.IsNullOrEmpty(KoiFish.Gender))
            {
                KoiFish.Gender = "ALL";
            }

            if (string.IsNullOrEmpty(KoiFish.Type))
            {
                KoiFish.Type = "ALL";
            }

            return Page();
        }
		public async Task<IActionResult> OnPost()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
			{
				return Page();
			}

			byte[] koiImage = null;

			KoiFish.KoiFishId =long.Parse(Request.Form["koiFishId"]);

			KoiFish.Name = Request.Form["KoiFish.Name"];
			KoiFish.Description = Request.Form["KoiFish.Description"];
			KoiFish.Dob = DateTime.Parse(Request.Form["KoiFish.Dob"]);
			KoiFish.Price = double.Parse(Request.Form["KoiFish.Price"]);
			KoiFish.Gender = Request.Form["gender"];
			KoiFish.Type = Request.Form["type"];
			KoiFish.UpdateDate = DateTime.Now;

			// If new image is uploaded, convert it to byte array
			if (KoiImage != null)
			{
				using (var memoryStream = new MemoryStream())
				{
					await KoiImage.CopyToAsync(memoryStream);
					koiImage = memoryStream.ToArray(); // Store image as byte array
					KoiFish.ImageData = koiImage; // Update the KoiFish's image
				}
			}
			else
			{
				
				var existingKoiFish = await _koiFishService.GetKoiFishByIdAsync(KoiFish.KoiFishId);
				KoiFish.ImageData = existingKoiFish?.ImageData; 
			}


			TempData["SuccessMessage"] = "Koi fish updated successfully.";
			await _koiFishService.UpdateKoiFishAsync(KoiFish);

		
			return RedirectToPage("/Staff/Index");
		}
	}
}
