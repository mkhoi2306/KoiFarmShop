using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Models.Items;
using KoiFarmShop.Service.IServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace KoiFarmShop.Service.Services
{
    public class KoiOrderService : IKoiOrderService
    {
        private readonly IKoiOrderRepository _orderRepository;
        private readonly KoiFarmShopContext _dbContext;
        private readonly ILogger<KoiOrderService> _logger;
        public KoiOrderService(IKoiOrderRepository orderRepository, KoiFarmShopContext dbContext, ILogger<KoiOrderService> logger)
        {
            _orderRepository = orderRepository;
            _dbContext = dbContext;
            _logger = logger;
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

        public async Task<List<KoiOrderDetail>> GetKoiOrderDetailsByOrderIdsAsync(List<long> koiOrderIds)
        {
            return await _dbContext.KoiOrderDetails
                .Where(kod => koiOrderIds.Contains((long)kod.KoiOrderId))
                .Include(kod => kod.KoiFish)
                .ToListAsync();
        }

        public async Task<List<KoiOrder>> GetAllOrdersByAccountAsync(long customerId)
        {
            try
            {
                var context = new KoiFarmShopContext();
                return await context.KoiOrders
                    .Include(o => o.KoiOrderDetails)
                    .Where(o => o.CustomerId == customerId)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception("An error occurred while retrieving orders: " + e.Message);
            }
        }
        public async Task<long> CreateOrderAsync(KoiOrder koiOrder)
        {
            using var context = new KoiFarmShopContext();
            var maxId = await context.KoiOrders.MaxAsync(u => (long?)u.KoiOrderId) ?? 0;
            long id = maxId + 1;
            koiOrder.KoiOrderId = id;

            // Thêm đơn hàng và lưu thay đổi
            await context.KoiOrders.AddAsync(koiOrder);
            await context.SaveChangesAsync();


            return koiOrder.KoiOrderId;
        }

        public async Task<KoiOrder> GetOrderAsync(long orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<ServiceResponse<bool>> DeleteOrderAsync(long id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(id);
                if (order == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        Message = "Order not found",
                        Data = false
                    };
                }

                await _orderRepository.DeleteAsync(id);
                await _orderRepository.SaveChangesAsync();

                _logger.LogInformation($"Order {id} was successfully deleted");

                return new ServiceResponse<bool>
                {
                    Data = true,
                    Message = "Order successfully deleted"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting order {id}: {ex.Message}");
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Message = "Error deleting order",
                    Data = false
                };
            }

        }
    }
}