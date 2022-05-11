using Eshop.Exceptions;
using Eshop.Models;
using Eshop.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContextService _userContext;

        public UserService(IUserRepository userRepository, IUserContextService userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task DepositMoney(decimal money)
        {
            var user = await _userRepository.GetById();
            user.Wallet = user.Wallet + money;

            if (user.Wallet > 999999.99m)
            {
                throw new BalanceExceededException();
            }

            await _userRepository.UpdateUser(user);
        }

        public async Task<AppUser> GetUserById()
        {
            var user = await _userRepository.GetById();
            return user;
        }
        public async Task<AppUser> GetUserById(string userId)
        {
            var user = await _userRepository.GetById(userId);
            return user;
        }
    }
}
