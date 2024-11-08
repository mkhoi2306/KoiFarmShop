using KoiFarmShop.Repository.IRepo;
using KoiFarmShop.Repository.Models;
using KoiFarmShop.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiFarmShop.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public async Task<bool> CreateAccoutnForGuest(User user)
        {
            return await _userRepository.CreateAccountForGuest(user);
        }

        public async Task<Customer> GetCustomerByUser(long userId)
        {
            return await _userRepository.GetCustomerByUserId(userId);
        }

        public long GetNextUserId()
        {
            return _userRepository.GetNextUserId();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userRepository.GetUserByEmail(email);
        }

        public Task<User> GetUserById(long userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public Task<bool> ResetPassword(User user)
        {
            return _userRepository.ResetPasswordForCustomer(user);
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> CreateUserAsync(User user)
        {
            return await _userRepository.CreateAccountAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
        public async Task<User> GetUserByIdAsync(long id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }

    }
}
