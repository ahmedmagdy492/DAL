using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class FormView : BaseModel
    {
        public string FormName { get; set; }
        public IList<RoleForms> RoleForms { get; set; }
    }
}
