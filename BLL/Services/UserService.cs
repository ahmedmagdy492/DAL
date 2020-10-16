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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepo;
        private readonly IPasswordHashing _passwordHashing;
        private readonly UnitOfWork _unitOfWork;

        public UserService()
        {
            _unitOfWork = new UnitOfWork(SharedLib.DBContextCreator.DbContext);
            _userRepo = _unitOfWork.CreateUserRepo();
            _passwordHashing = new Sha256Hashing();
        }

        public User AddOrUpdate(User user)
        {
            var updatedUser = _userRepo.Update(user);
            _unitOfWork.Commit();
            return updatedUser;
        }

        public User AddUser(User user)
        {
            user.Password = _passwordHashing.Hash(user.Password);
            _userRepo.Add(user);
            _unitOfWork.Commit();
            return user;
        }

        public Task<User> GetUserByUsername(string username)
        {
            return Task.Run(() =>
            {
                return _userRepo.GetAll().FirstOrDefault(u => u.Username == username);
            });
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepo.GetAll();
        }
    }
}
