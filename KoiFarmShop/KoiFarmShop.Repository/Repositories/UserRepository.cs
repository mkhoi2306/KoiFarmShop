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

        public async Task<bool> ResetPasswordForCustomer(User user)
        {
            var existUser = await _context.Users.FindAsync(user.UserId);
            if (existUser != null)
            {
                existUser.Password = user.Password;
                _context.Users.Update(existUser);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<User> CreateAccountAsync(User user)
        {
            user.UserId = GetNextUserId();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
