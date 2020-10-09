using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
        bool AddUserToRole(int roleId, int userId);
        bool IsUserExistInRole(string roleName, int userId);
        Role AddRole(Role role);
        void RemoveRole(Role role);
        bool AddFormToRole(int roleId, int formId);
        bool RemoveFromFromRole(int roleId, int formId);
        Role GetRoleByName(string roleName);
    }
}
