using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Role : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public IList<User> Users { get; set; }
        public IList<FormView> FormViews { get; set; }
    }
}
