﻿using KoiFarmShop.Repository.Models;
using KoiFarmShop.Repository.Models.Items;
using KoiFarmShop.Service.Services;
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
        Task<List<KoiOrderDetail>> GetKoiOrderDetailsByOrderIdsAsync(List<long> koiOrderIds);
        Task<ServiceResponse<bool>> DeleteOrderAsync(long id);
    }
}
