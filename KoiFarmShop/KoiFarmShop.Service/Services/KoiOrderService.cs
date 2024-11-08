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

        public async Task<KoiOrder> CancelOrderAsync(long orderId, string cancelledBy)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                throw new Exception("Order not found");
            }

            // Kiểm tra xem order có thể hủy không
            if ((bool)order.IsDeleted)
            {
                throw new Exception("Order already cancelled");
            }

            // Cập nhật trạng thái order
            order.IsDeleted = true;
            order.UpdatedDate = DateTime.UtcNow;
            order.UpdatedBy = cancelledBy;

            // Cập nhật trạng thái của tất cả order details
            foreach (var orderDetail in order.KoiOrderDetails)
            {
                orderDetail.IsDeleted = true;
                orderDetail.UpdatedDate = DateTime.UtcNow;
                orderDetail.UpdatedBy = cancelledBy;
            }

            // Lưu các thay đổi
            return await _orderRepository.UpdateOrderAsync(order);
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
