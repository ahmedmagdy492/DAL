using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IPasswordChangingService
    {
        /// <summary>
        /// Changes the password of the user if the user credentials is valid
        /// </summary>
        /// <returns>Returns true if the operation is successful and false if not</returns>
        bool ChangePassword(User user, string newPassword);
    }
}
