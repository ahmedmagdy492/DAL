using DAL.Models;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FormService : IFormService
    {
        private readonly DAL.UnitOfWork _unitOfWork;
        private readonly IRepository<FormView> _formViewRepo;
        public FormService()
        {
            _unitOfWork = SharedLib.UnitOfWorkCreator.Instance;
            _formViewRepo = _unitOfWork.CreateFormViewRepo();
        }

        public FormView AddForm(FormView formView)
        {
            return _formViewRepo.Add(formView);
        }
    }
}
