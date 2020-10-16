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
    public class OrderService : IOrderService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IRepository<Order> _orderRepo;

        public OrderService()
        {
            _unitOfWork = new UnitOfWork(SharedLib.DBContextCreator.DbContext);
            _orderRepo = _unitOfWork.CreateOrderRepo();
        }

        public Order Add(Order model)
        {
            var order = _orderRepo.Add(model);
            _unitOfWork.Commit();
            return order;
        }

        public void Delete(Order model)
        {
            _orderRepo.Delete(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepo.GetAll();
        }

        public Order GetById(int id)
        {
            return _orderRepo.GetById(id);
        }

        public Order Update(Order model)
        {
            var order = _orderRepo.Update(model);
            _unitOfWork.Commit();
            return order;
        }
    }
}
