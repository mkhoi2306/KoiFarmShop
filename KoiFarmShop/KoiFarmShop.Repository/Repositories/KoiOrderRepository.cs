using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.Repositories
{
    public class KoiOrderRepository : IKoiOrderRepository
    {
        private readonly KoiFarmShopContext _context;

        public KoiOrderRepository(KoiFarmShopContext context)
        {
            _context = context;
        }

        public async Task<KoiOrder> CreateOrderAsync(KoiOrder order)
        {
            _context.KoiOrders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<KoiOrderDetail> CreateOrderDetailAsync(KoiOrderDetail orderDetail)
        {
            _context.KoiOrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<KoiOrder> GetOrderByIdAsync(long orderId)
        {
            return await _context.KoiOrders
            .Include(o => o.KoiOrderDetails)
                .ThenInclude(od => od.KoiFish)
            .FirstOrDefaultAsync(o => o.KoiOrderId == orderId);
        }

        public async Task<IEnumerable<KoiOrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId)
        {
            return await _context.KoiOrderDetails
            .Include(od => od.KoiFish)
            .Where(od => od.KoiOrderId == orderId)
            .ToListAsync();
        }

        public async Task<KoiOrder> UpdateOrderAsync(KoiOrder order)
        {
            _context.KoiOrders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
    
}
