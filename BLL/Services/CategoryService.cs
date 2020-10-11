using DAL;
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
    public class CategoryService : ICategoryService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IRepository<Category> _cateRepo;

        public CategoryService()
        {
            _unitOfWork = UnitOfWorkCreator.Instance;
            _cateRepo = _unitOfWork.CreateCategoryRepo();
        }

        public Category Add(Category model)
        {
            var createdModel = _cateRepo.Add(model);
            _unitOfWork.Commit();
            return createdModel;
        }

        public void Delete(Category model)
        {
            _cateRepo.Delete(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<Category> GetAll()
        {
            return _cateRepo.GetAll();
        }

        public Category GetById(int id)
        {
            return _cateRepo.GetById(id);
        }

        public Category Update(Category model)
        {
            var updatedModel = _cateRepo.Update(model);
            _unitOfWork.Commit();
            return updatedModel;
        }
    }
}
