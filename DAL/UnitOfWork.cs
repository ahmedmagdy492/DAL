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

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        public IRepository<T> CreateRepository<T>() where T : BaseModel
        {
            return new Repository<T>(_context);
        }
    }
}
