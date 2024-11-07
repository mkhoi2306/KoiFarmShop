using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.Repository;

public interface IKoiFishRepository
{
    Task<List<KoiFish>> GetAllAsync();
    Task<KoiFish> GetByIdAsync(long id);
    Task AddAsync(KoiFish koiFish);
    Task UpdateAsync(KoiFish koiFish);
    Task DeleteAsync(long id);
}