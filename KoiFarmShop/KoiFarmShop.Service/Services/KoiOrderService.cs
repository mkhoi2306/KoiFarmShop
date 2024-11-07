using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Models.Items;
using KoiFarmShop.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Service.Services
{
    public class KoiOrderService : IKoiOrderService
    {

        private readonly IKoiOrderRepository _orderRepository;

        public KoiOrderService(IKoiOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        public async Task<KoiOrder> CreateOrderAsync(long customerId, List<CartItem> cartItems, string createdBy)
        {
            double totalPrice = cartItems.Sum(item => item.Price * item.Quantity);
            int totalQuantity = cartItems.Sum(item => item.Quantity);

            // Create order
            var order = new KoiOrder
            {
                CustomerId = customerId,
                Price = totalPrice,
                Quantity = totalQuantity,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = createdBy,
                IsDeleted = false
            };

            // Save order
            order = await _orderRepository.CreateOrderAsync(order);

            // Create order details
            foreach (var item in cartItems)
            {
                var orderDetail = new KoiOrderDetail
                {
                    KoiOrderId = order.KoiOrderId,
                    KoiFishId = item.KoiFishId,
                    TotalPrice = item.Price * item.Quantity,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = createdBy,
                    IsDeleted = false
                };

                await _orderRepository.CreateOrderDetailAsync(orderDetail);
            }

            return order;
        }

        public async Task<KoiOrder> GetOrderAsync(long orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }
    }
}
