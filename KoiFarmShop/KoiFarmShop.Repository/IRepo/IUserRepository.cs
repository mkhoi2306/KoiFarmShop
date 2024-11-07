using KoiFarmShop.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Repository.IRepo
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmail(string email);

        Task<bool> CreateAccountForGuest(User user);

        Task<User> GetUserById(long userId);

        long GetNextUserId();
    }

}
