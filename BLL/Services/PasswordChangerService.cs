using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PasswordChangerService : IPasswordChangingService
    {
        private readonly IPasswordHashing _passwordHashing;
        private readonly IUserService _userService;
        public PasswordChangerService()
        {
            _passwordHashing = new Sha256Hashing();
            _userService = new UserService();
        }

        public bool ChangePassword(User user, string newPassword)
        {
            var existingUser = _userService.GetUserByUsername(user.Username);

            if (existingUser == null)
                return false;

            string givenPasswordHash = _passwordHashing.Hash(user.Password);

            if (givenPasswordHash != user.Password)
                return false;

            string newPasswordHash = _passwordHashing.Hash(newPassword);
            user.Password = newPasswordHash;
            var updatedUser = _userService.AddOrUpdate(user);
            return updatedUser == null ? false : true;
        }
    }
}
