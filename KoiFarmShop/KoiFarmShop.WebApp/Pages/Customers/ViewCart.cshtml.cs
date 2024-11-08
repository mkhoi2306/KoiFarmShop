using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Models.Items;
using KoiFarmShop.Service;
using KoiFarmShop.Service.IServices;
using KoiFarmShop.WebApp.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace KoiFarmShop.WebApp.Pages.Customers
{
    public class ViewCartModel : PageModel
    {
        public Cart cart;
        private const string CartSessionKey = "Cart";

        private readonly IKoiFishService _koiFishService;
        private readonly IKoiOrderService _koiOrderService;

        private readonly IKoiOrderDetailService _koiOrderDetailService;

        public ViewCartModel(IKoiFishService koiFishService, IKoiOrderService koiOrderService,
            IKoiOrderDetailService koiOrderDetailService)
        {
            _koiFishService = koiFishService;
            _koiOrderService = koiOrderService;
            this._koiOrderDetailService = koiOrderDetailService;
        }

        public List<KoiFish> KoiFishes { get; set; } = new List<KoiFish>();
        public double? totalAmount { get; set; } = default!;

        public async Task OnGet()
        {
            if (HttpContext.Session != null)
            {
                cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
            }
            else
            {
                cart = new Cart();
            }

            double? amounts = 0;
            foreach (var item in cart.Items)
            {
                KoiFish koiFish = await _koiFishService.GetKoiFishByIdAsync(item.KoiId);
                KoiFishes.Add(koiFish);
                double? amount = koiFish.Price * item.Quantity;
                amounts += amount;
            }

            totalAmount = amounts;
        }

        public IActionResult OnPostUpdateCart(long KoiFishId, int Quantity)
        {
            var cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();

            var koiFish = cart.Items.FirstOrDefault(item => item.KoiId == KoiFishId);
            if (koiFish != null && Quantity > 0)
            {
                koiFish.Quantity = Quantity;
            }
            else if (koiFish != null && Quantity == 0)
            {
                cart.RemoveKoi(cart.Items.FirstOrDefault(item => item.KoiId == KoiFishId));
            }

            SaveCart(cart);
            return RedirectToPage();
        }

        public IActionResult OnPostRemoveItem(long KoiFishId)
        {
            var cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
            cart.RemoveKoi(cart.Items.FirstOrDefault(item => item.KoiId == KoiFishId));
            SaveCart(cart);
            return RedirectToPage();
        }

        private void SaveCart(Cart cartItems)
        {
            HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cartItems));
        }

        public async Task<IActionResult> OnPostCreateOrder()
        {
            // Lấy giỏ hàng từ session
            cart = HttpContext.Session.GetObject<Cart>(CartSessionKey) ?? new Cart();
            if (cart.Items.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Your cart is empty.");
                return RedirectToPage();
            }


            int totalQuantity = cart.Items.Sum(item => item.Quantity);


            if (User?.Identity?.IsAuthenticated != true)
            {
                ModelState.AddModelError(string.Empty, "User is not authenticated.");
                return RedirectToPage("/Account/Login");
            }


            long customerId = long.Parse(User.FindFirst("customerId").Value);
            KoiOrder koiOrder = new KoiOrder
            {
                CustomerId = customerId,
                Quantity = totalQuantity,
                Price = cart.TotalAmount(),
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                IsDeleted = false
            };

            long koiOrderId = await _koiOrderService.CreateOrderAsync(koiOrder);

            foreach (var item in cart.Items)
            {
                Console.WriteLine($"Creating KoiOrderDetail for KoiId: {item.KoiId}");
                KoiOrderDetail koiOrderDetail = new KoiOrderDetail
                {
                    KoiOrderId = koiOrderId,
                    KoiFishId = item.KoiId,
                    TotalPrice = item.price * item.Quantity,
                    CreatedDate = DateTime.Now,
                    UpdatedDate = DateTime.Now,
                    IsDeleted = false
                };

                await _koiOrderDetailService.CreateOrderDetail(koiOrderDetail);
            }

            // Xóa giỏ hàng sau khi tạo đơn hàng
            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToPage("/Customers/ViewOrder");
        }
    }
}