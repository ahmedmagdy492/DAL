using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Logs the user in after verfiying the provided credentials
        /// </summary>
        /// <returns>Returns the logged in user object if not found returns null</returns>
        Task<User> Login(string username, string password);

        /// <summary>
        /// Create a new user with the given user information. it will fail if the user is already exist
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Returns the created user in case of failure it returns null</returns>
        Task<User> Register(User user);

        /// <summary>
        /// Checks if the application is the first time to open if so an admin account should be registered otherwise a normal login
        /// </summary>
        /// <returns>
        /// Returns true if no users yet, otherwise returns false
        /// </returns>
        bool IsFirstTime();
    }
}
