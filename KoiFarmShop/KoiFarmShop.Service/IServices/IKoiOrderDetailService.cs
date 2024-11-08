using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.Service.IServices;

public interface IKoiOrderDetailService
{
    Task CreateOrderDetail(KoiOrderDetail orderDetail);
}