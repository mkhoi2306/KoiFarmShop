using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.Repository;

public interface IKoiFishRepository
{
    Task<IEnumerable<KoiFish>> GetAllAsync();
    Task<KoiFish> GetByIdAsync(long? id);
    Task AddAsync(KoiFish koiFish);
    Task UpdateAsync(KoiFish koiFish);
    Task DeleteAsync(int id);
    long GetnextKoiFishId();
}