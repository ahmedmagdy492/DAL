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
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItems> _orderItemsService;
        private readonly UnitOfWork _unitOfWork;

        public OrderItemService()
        {
            _unitOfWork = new UnitOfWork(SharedLib.DBContextCreator.DbContext);
            _orderItemsService = _unitOfWork.CreateOrderItemsRepo();
        }

        public OrderItems Add(OrderItems model)
        {
            var orderItem = _orderItemsService.Add(model);
            _unitOfWork.Commit();
            return orderItem;
        }

        public void Delete(OrderItems model)
        {
            _orderItemsService.Delete(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<OrderItems> GetAll()
        {
            return _orderItemsService.GetAll();
        }

        public OrderItems GetById(int id)
        {
            return _orderItemsService.GetById(id);
        }

        public OrderItems Update(OrderItems model)
        {
            var orderItem = _orderItemsService.Update(model);
            _unitOfWork.Commit();
            return orderItem;
        }
    }
}
