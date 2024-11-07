using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private KoiFarmShopContext _context;

        public UserRepository(KoiFarmShopContext context)
        {
            this._context = context;
        }

        public async Task<bool> CreateAccountForGuest(User user)
        {
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public long GetNextUserId()
        {
            var maxuserId = _context.Users.Max(x => (long?)x.UserId) ?? 0;
            return maxuserId + 1;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Include(u => u.Customers).FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetUserById(long userId)
        {
            return await _context.Users.FirstAsync(x => x.UserId == userId);
        }
    }
}
