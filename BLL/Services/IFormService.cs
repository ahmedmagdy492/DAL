using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IFormService
    {
        /// <summary>
        /// Adds a new Form View
        /// </summary>
        /// <param name="formView"></param>
        /// <returns>Returns the Created FormView Object if is already exist it returns null</returns>
        FormView AddForm(FormView formView);
    }
}
