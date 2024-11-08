using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiFarmShop.WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IKoiFishService _koiFishService;

        public IndexModel(ILogger<IndexModel> logger, IKoiFishService koiFishService)
        {
            _logger = logger;
            _koiFishService = koiFishService;
        }

        public List<KoiFishViewModel> KoiFishList { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var koiFishes = await _koiFishService.GetAllKoiFishAsync();
            
            KoiFishList = new List<KoiFishViewModel>();
            foreach (var koiFish in koiFishes)
            {
                KoiFishList.Add(new KoiFishViewModel
                {
                    KoiFishId = koiFish.KoiFishId,
                    Name = koiFish.Name,
                    Price = koiFish.Price,
                    ImageUrl = koiFish.ImageData != null 
                        ? $"data:image/jpeg;base64,{Convert.ToBase64String(koiFish.ImageData)}"
                        : "https://example.com/default-image.jpg"
                });
            }

            return Page();
        }

    }

    
    public class KoiFishViewModel
    {
        public long KoiFishId { get; set; }
        public string Name { get; set; }
        public double? Price { get; set; }
        public string ImageUrl { get; set; }
    }
}