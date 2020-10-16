using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            dbSet = _context.Set<T>();
        }

        public T Add(T model)
        {
            dbSet.Add(model);
            return model;
        }

        public void Delete(T model)
        {
            dbSet.Remove(model);
        }

        public IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public T Update(T model)
        {
            dbSet.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            return model;
        }
    }
}
