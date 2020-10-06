using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IRepository<TModel> where TModel : BaseModel
    {
        IEnumerable<TModel> GetAll();
        TModel GetById(int id);
        TModel Add(TModel model);
        void Delete(TModel model);
        TModel Update(TModel model);
        bool Commit();
    }
}
