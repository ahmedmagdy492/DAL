using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserService
    {
        Task<User> GetUserByUsername(string username);
        User AddUser(User user);
        User AddOrUpdate(User user);
        IEnumerable<User> GetUsers();
    }
}
