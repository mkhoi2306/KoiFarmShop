using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiFarmShop.Repository.Models;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace KoiFarmShop.WebApp.Pages.Staff
{
	public class CreateModel : PageModel
	{
		private readonly KoiFarmShop.Repository.Models.KoiFarmShopContext _context;


		[BindProperty]
		public IFormFile KoiImage { get; set; }

		public CreateModel(KoiFarmShop.Repository.Models.KoiFarmShopContext context)
		{
			_context = context;
		}

		public IActionResult OnGet()
		{
			ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
			ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
			return Page();
		}

		[BindProperty]
		public KoiFish KoiFish { get; set; } = default!;

		// For more information, see https://aka.ms/RazorPagesCRUD.
		//public async Task<IActionResult> OnPostAsync()
		//{
		//    if (!ModelState.IsValid)
		//    {
		//        return Page();
		//    }

		//    _context.KoiFishes.Add(KoiFish);
		//    await _context.SaveChangesAsync();

		//    return RedirectToPage("./Index");
		//}

		public IActionResult OnPost()
		{
			KoiFish koiFish = new KoiFish();
			koiFish.KoiFishId = 1;
			byte[] koiImage = null;
			if (KoiImage != null)
			{
				 using (var memoryStream = new MemoryStream())
            {
                KoiImage.CopyTo(memoryStream);
                 koiImage = memoryStream.ToArray();
            }
				koiFish.ImageData = koiImage;
				koiFish.Type = Request.Form["statusConsignment"];
				_context.KoiFishes.Add(koiFish);
				_context.SaveChanges();
				return RedirectToPage("/Index");
			}
			else
			{
				ModelState.AddModelError("KoiImage", "Please upload an image.");
				return Page();  // Quay l?i trang n?u không có ?nh
			}

		}
	}
}
