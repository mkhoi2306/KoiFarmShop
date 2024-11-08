using KoiFarmShop.Repository;
using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.Service;

public class KoiFishService : IKoiFishService
{
    private readonly IKoiFishRepository _koiFishRepository;

    public KoiFishService(IKoiFishRepository koiFishRepository)
    {
        _koiFishRepository = koiFishRepository;
    }

    public async Task<IEnumerable<KoiFish>> GetAllKoiFishAsync()
    {
        return await _koiFishRepository.GetAllAsync();
    }

    public async Task<KoiFish> GetKoiFishByIdAsync(long id)
    {
        return await _koiFishRepository.GetByIdAsync(id);
    }

    public async Task AddKoiFishAsync(KoiFish koiFish)
    {
        await _koiFishRepository.AddAsync(koiFish);
    }

    public async Task UpdateKoiFishAsync(KoiFish koiFish)
    {
        await _koiFishRepository.UpdateAsync(koiFish);
    }

    public async Task DeleteKoiFishAsync(int id)
    {
        await _koiFishRepository.DeleteAsync(id);
    }

    public long GetKoiFishId()
    {
        return _koiFishRepository.GetnextKoiFishId();
        
    }
}
