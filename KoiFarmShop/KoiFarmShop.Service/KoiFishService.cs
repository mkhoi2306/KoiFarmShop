using KoiFarmShop.Repository;
using KoiFarmShop.Repository.Models;

namespace KoiFarmShop.Service;

public class KoiFishService
{
    private readonly IKoiFishRepository _koiFishRepository;

    public KoiFishService(IKoiFishRepository koiFishRepository)
    {
        _koiFishRepository = koiFishRepository;
    }

    public async Task<List<KoiFish>> GetAllAsync()
    {
        return await _koiFishRepository.GetAllAsync();
    }

    public async Task<KoiFish> GetByIdAsync(long id)
    {
        return await _koiFishRepository.GetByIdAsync(id);
    }

    public async Task AddAsync(KoiFish koiFish)
    {
        await _koiFishRepository.AddAsync(koiFish);
    }

    public async Task UpdateAsync(KoiFish koiFish)
    {
        await _koiFishRepository.UpdateAsync(koiFish);
    }

    public async Task DeleteAsync(long id)
    {
        await _koiFishRepository.DeleteAsync(id);
    }
}