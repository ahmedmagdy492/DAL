using DAL;
using DAL.Data;
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
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _prodsRepo;
        private readonly UnitOfWork _unitOfWork;

        public ProductService()
        {
            _unitOfWork = new UnitOfWork(SharedLib.DBContextCreator.DbContext);
            _prodsRepo = _unitOfWork.CreateProductRepo();
        }

        public Product Add(Product model)
        {
            var createdModel = _prodsRepo.Add(model);
            _unitOfWork.Commit();
            return createdModel;
        }

        public void Delete(Product model)
        {
            _prodsRepo.Delete(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<Product> GetAll()
        {
            return _prodsRepo.GetAll();
        }

        public Product GetById(int id)
        {
            return _prodsRepo.GetById(id);
        }

        public Product Update(Product model)
        {
            var updatedModel = _prodsRepo.Update(model);
            _unitOfWork.Commit();
            return updatedModel;
        }
    }
}
