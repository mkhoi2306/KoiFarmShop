using System;
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
        private readonly IKoiFishService _koiFishService;

        public EditModel(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        [BindProperty]
        public KoiFish KoiFish { get; set; }

        public async Task<IActionResult> OnGetAsync(long id)
        {
            KoiFish = await _koiFishService.GetKoiFishByIdAsync(id);
            if (KoiFish == null)
            {
                return RedirectToPage("/Staff/Index");
            }

            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page(); 
            }
            await _koiFishService.UpdateKoiFishAsync(KoiFish);
            return RedirectToPage("/Staff/Index");
        }
    }
}
