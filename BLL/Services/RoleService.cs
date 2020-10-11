using DAL;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> _roleRepo;
        private readonly IRepository<RoleForms> _formRepo;
        private readonly IRepository<User> _userRepo;
        private readonly UnitOfWork _unitOfWork;

        public RoleService()
        {
            _unitOfWork = SharedLib.UnitOfWorkCreator.Instance;
            _roleRepo = _unitOfWork.CreateRoleRepo();
            _formRepo = _unitOfWork.CreateRoleFormsRepo();
            _userRepo = _unitOfWork.CreateUserRepo();
        }

        public bool AddFormToRole(int roleId, int formId)
        {
            // checking if the form is already exist in this role
            var roleForm = _formRepo.GetAll().FirstOrDefault(rf => rf.RoleId == roleId && rf.FormId == formId);

            if (roleForm != null) return false;

            // adding a new form role
            var newRoleForm = new RoleForms
            {
                FormId = formId,
                RoleId = roleId
            };
            _formRepo.Add(newRoleForm);
            _unitOfWork.Commit();
            return true;
        }

        public Role AddRole(Role role)
        {
            // checking if the role already exist or not
            var existingRole = _roleRepo.GetAll().FirstOrDefault(r => r.Name == role.Name);

            if (existingRole != null) return null;

            _roleRepo.Add(role);
            _unitOfWork.Commit();
            return role;
        }

        public bool AddUserToRole(int roleId, int userId)
        {
            // checking if the user is already exist in a role
            var user = _userRepo.GetAll().FirstOrDefault(u => u.RoleId == roleId && u.Id == userId);

            // changing the user role id to be set to the one
            user.RoleId = roleId;
            _userRepo.Update(user);
            _unitOfWork.Commit();
            return true;
        }

        public Role GetRoleByName(string roleName)
        {
            return _roleRepo.GetAll().FirstOrDefault(r => r.Name == roleName);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _roleRepo.GetAll();
        }

        public bool IsUserExistInRole(string roleName, int userId)
        {
            var role = _roleRepo.GetAll().FirstOrDefault(r => r.Name == roleName);
            var user = _userRepo.GetAll().FirstOrDefault(u => u.RoleId == role.Id && u.Id == userId);

            return user == null ? false : true;
        }

        public bool RemoveFromFromRole(int roleId, int formId)
        {
            // checking if the form is attached to that role
            var roleForm = _formRepo.GetAll().FirstOrDefault(fr => fr.FormId == formId && fr.RoleId == roleId);

            if (roleForm == null) return false;

            _formRepo.Delete(roleForm);
            _unitOfWork.Commit();
            return true;
        }

        public void RemoveRole(Role role)
        {
            // checking if the role is already exist
            var existingRole = _roleRepo.GetById(role.Id);

            if (existingRole == null) return;

            _roleRepo.Delete(role);
            _unitOfWork.Commit();
        }
    }
}
