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

        Task<bool> ResetPasswordForCustomer(User user);

        Task<User> CreateAccountAsync(User user);
        Task UpdateAsync(User user);
        Task<User> GetUserByIdAsync(long id);
        Task<bool> DeleteAsync(long userId);

        Task<IEnumerable<User>> GetAllAsync();

        Task<Customer> GetCustomerByUserId(long userId);
    }

}
