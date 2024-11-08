using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.Service;

public interface IKoiFishService
{
    Task<IEnumerable<KoiFish>> GetAllKoiFishAsync();
    Task<KoiFish> GetKoiFishByIdAsync(long id);
    Task AddKoiFishAsync(KoiFish koiFish);
    Task UpdateKoiFishAsync(KoiFish koiFish);
    Task DeleteKoiFishAsync(int id);

    long GetKoiFishId();

}