using DAL.Data;
using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Category> _categoryRepo;
        private IRepository<FormView> _formViewRepo;
        private IRepository<Role> _roleRepo;
        private IRepository<User> _userRepo;
        private IRepository<Product> _productRepo;
        private IRepository<Order> _orderRepo;
        private IRepository<OrderItems> _orderItemsRepo;

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        public IRepository<Category> CreateCategoryRepo()
        {
            if(_categoryRepo == null)
            {
                _categoryRepo = new Repository<Category>(_context);
                return _categoryRepo;
            }
            return _categoryRepo;
        }
        public IRepository<FormView> CreateFormViewRepo()
        {
            if (_formViewRepo == null)
            {
                _formViewRepo = new Repository<FormView>(_context);
                return _formViewRepo;
            }
            return _formViewRepo;
        }
        public IRepository<Role> CreateRoleRepo()
        {
            if (_roleRepo == null)
            {
                _roleRepo = new Repository<Role>(_context);
                return _roleRepo;
            }
            return _roleRepo;
        }
        public IRepository<User> CreateUserRepo()
        {
            if (_userRepo == null)
            {
                _userRepo = new Repository<User>(_context);
                return _userRepo;
            }
            return _userRepo;
        }
        public IRepository<Product> CreateProductRepo()
        {
            if (_productRepo == null)
            {
                _productRepo = new Repository<Product>(_context);
                return _productRepo;
            }
            return _productRepo;
        }
        public IRepository<Order> CreateOrderRepo()
        {
            if (_orderRepo == null)
            {
                _orderRepo = new Repository<Order>(_context);
                return _orderRepo;
            }
            return _orderRepo;
        }
        public IRepository<OrderItems> CreateOrderItemsRepo()
        {
            if (_orderItemsRepo == null)
            {
                _orderItemsRepo = new Repository<OrderItems>(_context);
                return _orderItemsRepo;
            }
            return _orderItemsRepo;
        }
    }
}
