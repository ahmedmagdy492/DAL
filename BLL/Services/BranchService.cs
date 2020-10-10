using DAL;
using DAL.Models;
using DAL.Repository;
using SharedLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Services
{
    public class BranchService : IBranchService
    {
        private IRepository<Branch> _branchRepo;
        public BranchService()
        {
            UnitOfWork unitOfWork = UnitOfWorkCreator.Instance;
            _branchRepo = unitOfWork.CreateBranchRepo();
        }
        public Branch Add(Branch model)
        {
            var branch = _branchRepo.Add(model);
            _branchRepo.Commit();
            return branch;
        }

        public void ChangeOnlineStatus(int branchId, bool isOnline)
        {
            var branch = _branchRepo.GetById(branchId);
            branch.IsOnline = isOnline;
            _branchRepo.Update(branch);
            _branchRepo.Commit();
        }

        public bool Commit()
        {
            return _branchRepo.Commit();
        }

        public void Delete(Branch model)
        {
            _branchRepo.Delete(model);
            _branchRepo.Commit();
        }

        public IEnumerable<Branch> GetAll()
        {
            return _branchRepo.GetAll();
        }

        public Branch GetById(int id)
        {
            return _branchRepo.GetById(id);
        }

        public Branch Update(Branch model)
        {
            return _branchRepo.Update(model);
        }
    }
}
