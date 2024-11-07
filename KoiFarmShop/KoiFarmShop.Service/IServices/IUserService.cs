using KoiFarmShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Service.IServices
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);

        Task<bool> CreateAccoutnForGuest(User user);

        Task<User> GetUserById(long userId);

        long GetNextUserId();
    }
}
