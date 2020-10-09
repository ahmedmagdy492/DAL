using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class RoleForms : BaseModel
    {
        [Column(Order = 0)]
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }

        [Column(Order = 1)]
        [ForeignKey(nameof(FormView))]
        public int FormId { get; set; }
        public Role Role { get; set; }
        public FormView FormView { get; set; }
    }
}
