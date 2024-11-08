using KoiFarmShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Repository;

public class KoiFishRepository : IKoiFishRepository
{
    private readonly KoiFarmShopContext _context;

    public KoiFishRepository(KoiFarmShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<KoiFish>> GetAllAsync()
    {
        return await _context.KoiFishes.ToListAsync();
    }

    public async Task<KoiFish> GetByIdAsync(int id)
    {
        return await _context.KoiFishes.FindAsync(id);
    }

    public async Task AddAsync(KoiFish koiFish)
    {
        await _context.KoiFishes.AddAsync(koiFish);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(KoiFish koiFish)
    {
        _context.KoiFishes.Update(koiFish);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var koiFish = await _context.KoiFishes.FindAsync(id);
        if (koiFish != null)
        {
            _context.KoiFishes.Remove(koiFish);
            await _context.SaveChangesAsync();
        }
    }

    public long GetnextKoiFishId()
    {
        var maxId = _context.KoiFishes.Max(k => (long?)k.KoiFishId) ?? 0;
        return maxId + 1;
    }
}
