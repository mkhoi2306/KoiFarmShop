using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.WebApp.Pages.Staff
{
    public class DetailsModel : PageModel
    {
        private readonly KoiFarmShopContext _context;

        public DetailsModel(KoiFarmShopContext context)
        {
            _context = context;
        }

        public KoiFish KoiFish { get; set; }

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
                return NotFound();

            KoiFish = await _context.KoiFishes
                .Include(k => k.Category)
                .Include(k => k.Size)
                .FirstOrDefaultAsync(m => m.KoiFishId == id);

            if (KoiFish == null)
                return NotFound();

            return Page();
        }
    }
}