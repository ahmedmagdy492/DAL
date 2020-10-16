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
    public class LoggerService : ILoggerService
    {
        private readonly IRepository<Logging> _logger;
        private readonly UnitOfWork _unitOfWork;
        public LoggerService()
        {
            _unitOfWork = new UnitOfWork(SharedLib.DBContextCreator.DbContext);
            _logger = _unitOfWork.CreateLoggerRepo();
        }
        public Logging Add(Logging model)
        {
            var logger = _logger.Add(model);
            _unitOfWork.Commit();
            return logger;
        }

        public void Delete(Logging model)
        {
            _logger.Delete(model);
            _unitOfWork.Commit();
        }

        public IEnumerable<Logging> GetAll()
        {
            return _logger.GetAll();
        }

        public Logging GetById(int id)
        {
            return _logger.GetById(id);
        }

        public Logging Update(Logging model)
        {
            var updatedModel = _logger.Update(model);
            _unitOfWork.Commit();
            return updatedModel;
        }
    }
}
