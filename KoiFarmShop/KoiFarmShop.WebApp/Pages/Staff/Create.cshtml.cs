using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult OnGet()
        {
        ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
        ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
            return Page();
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.KoiFishes.Add(KoiFish);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
