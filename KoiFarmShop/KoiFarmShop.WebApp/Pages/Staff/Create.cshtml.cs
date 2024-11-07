using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.WebApp.Pages.Staff
{
    public class CreateModel : PageModel
    {
        private readonly KoiFarmShop.Repository.Models.KoiFarmShopContext _context;

        public CreateModel(KoiFarmShop.Repository.Models.KoiFarmShopContext context)
        {
            _context = context;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; } 
        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile? ImageFile)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Xử lý ảnh nếu có
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var fileName = Path.GetFileNameWithoutExtension(ImageFile.FileName);
                var extension = Path.GetExtension(ImageFile.FileName);
                var newFileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                //KoiFish.ImagePath = "/images/" + newFileName;
            }
            
            _context.KoiFishes.Add(KoiFish);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }



    }
}
