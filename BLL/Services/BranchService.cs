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
        private readonly UnitOfWork _unitOfWork;
        public BranchService()
        {
            _unitOfWork = new UnitOfWork(DBContextCreator.DbContext);
            _branchRepo = _unitOfWork.CreateBranchRepo();
        }
        public Branch Add(Branch model)
        {
            var branch = _branchRepo.Add(model);
            _unitOfWork.Commit();
            return branch;
        }

        public void ChangeOnlineStatus(int branchId, bool isOnline)
        {
            var branch = _branchRepo.GetById(branchId);
            branch.IsOnline = isOnline;
            _branchRepo.Update(branch);
            _unitOfWork.Commit();
        }

        public void Delete(Branch model)
        {
            _branchRepo.Delete(model);
            _unitOfWork.Commit();
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
            var updatedModel = _branchRepo.Update(model);
            _unitOfWork.Commit();
            return updatedModel;
        }

        public bool IsOnlineOrderingEnabled(int currentBranchId)
        {
            var currentBranch = _branchRepo.GetById(currentBranchId);
            return currentBranch == null ? false : currentBranch.IsOnline;
        }
    }
}
