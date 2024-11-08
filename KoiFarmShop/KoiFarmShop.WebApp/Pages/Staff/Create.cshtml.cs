using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace KoiFarmShop.WebApp.Pages.Staff
{
	public class CreateModel : PageModel
	{
		private readonly KoiFarmShop.Repository.Models.KoiFarmShopContext _context;
		private readonly IKoiFishService _koiFishService;


		[BindProperty]
		public IFormFile KoiImage { get; set; }

		public CreateModel(KoiFarmShop.Repository.Models.KoiFarmShopContext context, IKoiFishService koiFishService)
		{
			_context = context;
			_koiFishService = koiFishService;
		}
		
		public IActionResult OnGet()
		{
			ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
			ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
			return Page();
		}

		[BindProperty]
		public KoiFish koiFish { get; set; } = default!;

		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			
			koiFish.KoiFishId = GetKoiFishID();
			byte[] koiImage = null;
			if (KoiImage != null)
			{
				using (var memoryStream = new MemoryStream())
				{
					await KoiImage.CopyToAsync(memoryStream);
					koiImage = memoryStream.ToArray();
				}
				koiFish.CreateDate = DateTime.Now;
				koiFish.IsDeleted = false; 
				koiFish.ImageData = koiImage;
				koiFish.ImageData = koiImage;
				koiFish.Type = Request.Form["statusConsignment"];
				await _koiFishService.AddKoiFishAsync(koiFish);
				return RedirectToPage("/Staff/Index");
            {
            }
			}
			else
			{
				ModelState.AddModelError("KoiImage", "Please upload an image.");
				return Page();
			}
			
			await _koiFishService.AddKoiFishAsync(koiFish);
			
			return RedirectToPage("/Staff/Index");
		}

		private long GetKoiFishID()
		{
			return _koiFishService.GetKoiFishId();
		}
	}
}
