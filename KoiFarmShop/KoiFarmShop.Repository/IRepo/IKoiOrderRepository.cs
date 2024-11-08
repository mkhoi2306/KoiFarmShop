using KoiFarmShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.IRepo
{
    public interface IKoiOrderRepository
    {
        Task<KoiOrder> CreateOrderAsync(KoiOrder order);
        Task<KoiOrderDetail> CreateOrderDetailAsync(KoiOrderDetail orderDetail);
        Task<KoiOrder> GetOrderByIdAsync(long orderId);
        Task<IEnumerable<KoiOrderDetail>> GetOrderDetailsByOrderIdAsync(long orderId);
        Task<KoiOrder> UpdateOrderAsync(KoiOrder order);
    }
}
