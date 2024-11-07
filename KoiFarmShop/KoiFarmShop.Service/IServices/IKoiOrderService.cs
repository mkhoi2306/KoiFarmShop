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
        Task<KoiOrder> CreateOrderAsync(long customerId, List<CartItem> cartItems, string createdBy);
        Task<KoiOrder> GetOrderAsync(long orderId);
    }
}
