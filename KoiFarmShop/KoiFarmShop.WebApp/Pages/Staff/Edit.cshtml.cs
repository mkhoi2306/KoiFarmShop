using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;

namespace KoiFarmShop.WebApp.Pages.Staff
{
    public class EditModel : PageModel
    {
        private readonly KoiFarmShop.Repository.Models.KoiFarmShopContext _context;
        private readonly IKoiFishService _koiFishService;
        public EditModel(KoiFarmShop.Repository.Models.KoiFarmShopContext context, IKoiFishService koiFishService)
        {
            _context = context;
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koifish =  await _context.KoiFishes.FirstOrDefaultAsync(m => m.KoiFishId == id);
            if (koifish == null)
            {
                return NotFound();
            }
            KoiFish = koifish;
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId");
            ViewData["SizeId"] = new SelectList(_context.Sizes, "SizeId", "SizeId");
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(KoiFish).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KoiFishExists(KoiFish.KoiFishId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            await _koiFishService.UpdateKoiFishAsync(KoiFish);
            return RedirectToPage("/Index");
            
        }

        private bool KoiFishExists(long id)
        {
            return _context.KoiFishes.Any(e => e.KoiFishId == id);
        }
    }
}
