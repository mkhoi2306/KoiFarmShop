using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Models.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Service.IServices
{
    public interface IKoiOrderService
    {
        Task<long> CreateOrderAsync(KoiOrder order);
        Task<KoiOrder> GetOrderAsync(long orderId);
        Task<KoiOrder> CancelOrderAsync(long orderId, string cancelledBy);
        
        Task<List<KoiOrder>> GetAllOrdersByAccountAsync(long customerId);
    }
}
