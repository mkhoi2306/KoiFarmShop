using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Service.Services;

public class KoiOrderDetailService : IKoiOrderDetailService
{
    public async Task CreateOrderDetail(KoiOrderDetail orderDetail)
    {
        using var context = new KoiFarmShopContext();
        var maxId = await context.KoiOrderDetails.MaxAsync(u => (long?)u.KoiOrderDetailId) ?? 0;
        long id = maxId + 1;
        orderDetail.KoiOrderDetailId = id;

        // Thêm đơn hàng và lưu thay đổi
        await context.KoiOrderDetails.AddAsync(orderDetail);
        await context.SaveChangesAsync();
    }
}