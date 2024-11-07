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
    }
}
