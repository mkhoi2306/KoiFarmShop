using KoiFarmShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Repository;

public class KoiFishRepository
{
    private readonly KoiFarmShopContext _context;

    public KoiFishRepository(KoiFarmShopContext context)
    {
        _context = context;
    }

    public async Task<List<KoiFish>> GetAllAsync()
    {
        var sql = "SELECT * FROM KoiFish WHERE Is_Deleted = 0";  
        return await _context.KoiFishes
            .FromSqlRaw(sql)
            .ToListAsync();
    }
    
    public async Task<KoiFish> GetByIdAsync(long id)
    {
        return await _context.KoiFishes.FindAsync(id);
    }

    public async Task AddAsync(KoiFish koiFish)
    {
        _context.KoiFishes.Add(koiFish);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(KoiFish koiFish)
    {
        _context.KoiFishes.Update(koiFish);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var koiFish = await _context.KoiFishes.FindAsync(id);
        if (koiFish != null)
        {
            koiFish.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}