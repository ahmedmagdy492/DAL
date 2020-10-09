using DAL.Models;
using DAL.Repository;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHashing _passwordHashing;
        private readonly IUserService _userService;

        public AuthService(IPasswordHashing passwordHashing)
        {
            this._passwordHashing = passwordHashing;
            _userService = new UserService();
        }

        public bool IsFirstTime()
        {
            return _userService.GetUsers().Count() == 0 ? true : false;
        }

        public async Task<User> Login(string username, string password)
        {
            User user = await _userService.GetUserByUsername(username);

            if (user == null)
                return null;

            string givenPasswordHash = _passwordHashing.Hash(password);
            if (user.Password != givenPasswordHash)
                return null;

            return user;
        }

        public async Task<User> Register(User user)
        {
            User existingUser = await _userService.GetUserByUsername(user.Username);

            // hashing the password
            user.Password = _passwordHashing.Hash(user.Password);

            if (existingUser != null)
                return null;

            return _userService.AddUser(user);
        }
    }
}
