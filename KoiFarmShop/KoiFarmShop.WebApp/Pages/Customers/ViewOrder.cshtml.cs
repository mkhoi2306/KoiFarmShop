using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using KoiFarmShop.Service;

namespace KoiFarmShop.WebApp.Pages.Customers
{
    public class ViewOrderModel : PageModel
    {
        private readonly IKoiOrderService _koiOrderService;
        private readonly IKoiFishService _koiFishService;
        // private readonly IKoiOrderDetailService _koiOrderDetailService;

        public ViewOrderModel(IKoiOrderService koiOrderService, IKoiFishService koiFishService)
        {
            _koiOrderService = koiOrderService;
            _koiFishService = koiFishService;
            // _koiOrderDetailService = koiOrderDetailService;
        }
        public List<KoiOrderDetail> KoiOrderDetails { get; set; }
        public List<KoiOrder> KoiOrder { get;set; } = default!;
        public List<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();
        public decimal TotalPrice { get; set; }

        public async Task OnGetAsync()
        {
            string user = User.FindFirst("customerId").Value;
            KoiOrder = await _koiOrderService.GetAllOrdersByAccountAsync(long.Parse(user));

            // Load Koi Fish information
            var koiOrderDetails = await _koiOrderService.GetKoiOrderDetailsByOrderIdsAsync(KoiOrder.Select(o => o.KoiOrderId).ToList());
            var koiFishIds = koiOrderDetails.Select(od => od.KoiFishId).Distinct().ToList();
            var koiFishes = new Dictionary<long, KoiFish>();
            foreach (var id in koiFishIds)
            {
                var koiFish = await _koiFishService.GetKoiFishByIdAsync(id);
                koiFishes[id ?? 0] = koiFish;
            }

            // Calculate total price
            TotalPrice = (decimal)koiOrderDetails.Sum(od => od.TotalPrice);
        }
    }
}
