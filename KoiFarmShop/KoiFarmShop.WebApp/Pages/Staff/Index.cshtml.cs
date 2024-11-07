using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;

namespace KoiFarmShop.WebApp.Pages.Staff
{
    public class IndexModel : PageModel
    {
        private readonly IKoiFishService _koiFishService;

        public IndexModel(IKoiFishService koiFishService)
        {
            _koiFishService = koiFishService;
        }

        public IList<KoiFish> KoiFishes { get; set; }

        public async Task OnGetAsync()
        {
            KoiFishes = (await _koiFishService.GetAllKoiFishAsync()).ToList();
        }
    }
}
