using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.WebApp.Pages.Staff
{
    public class DetailsModel : PageModel
    {
        private readonly KoiFarmShop.Repository.Models.KoiFarmShopContext _context;

        public DetailsModel(KoiFarmShop.Repository.Models.KoiFarmShopContext context)
        {
            _context = context;
        }

        public KoiFish KoiFish { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var koifish = await _context.KoiFishes.FirstOrDefaultAsync(m => m.KoiFishId == id);
            if (koifish == null)
            {
                return NotFound();
            }
            else
            {
                KoiFish = koifish;
            }
            return Page();
        }
    }
}
