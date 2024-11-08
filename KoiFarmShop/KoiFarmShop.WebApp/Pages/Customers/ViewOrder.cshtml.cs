using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;

namespace KoiFarmShop.WebApp.Pages.Customers
{
    public class ViewOrderModel : PageModel
    {
        private readonly IKoiOrderService _koiOrderService;
        // private readonly IKoiOrderDetailService _koiOrderDetailService;

        public ViewOrderModel(IKoiOrderService koiOrderService)
        {
            _koiOrderService = koiOrderService;
            // _koiOrderDetailService = koiOrderDetailService;
        }

        public List<KoiOrder> KoiOrder { get;set; } = default!;

        public async Task OnGetAsync()
        {
            string user = User.FindFirst("customerId").Value;
            KoiOrder = await  _koiOrderService.GetAllOrdersByAccountAsync(long.Parse(user));
        }
    }
}
