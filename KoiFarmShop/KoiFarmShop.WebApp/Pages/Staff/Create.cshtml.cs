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
using Microsoft.AspNetCore.Authorization;

namespace KoiFarmShop.WebApp.Pages.Staff
{
	[Authorize(Roles = "Staff")]
	public class CreateModel : PageModel
	{
		private readonly IKoiFishService _koiFishService;


		[BindProperty]
		public IFormFile KoiImage { get; set; }

		[BindProperty]
		public KoiFish koiFish { get; set; } = default!;

		public CreateModel(IKoiFishService koiFishService)
		{
			_koiFishService = koiFishService;
		}

		public IActionResult OnGet()
		{
			// Khởi tạo ViewData và các thuộc tính khác cần thiết
			ViewData["Title"] = "Create KoiFish";
			koiFish = new KoiFish(); // Nếu koiFish cần thiết trong .cshtml
			return Page();
		}


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
				koiFish.Gender = Request.Form["gender"];
				koiFish.Type = Request.Form["type"];
				await _koiFishService.AddKoiFishAsync(koiFish);
				TempData["SuccessMessage"] = "Koi fish created successfully.";
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
