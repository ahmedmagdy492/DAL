using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Branch : BaseModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public bool IsOnline { get; set; }

        public IList<Order> Orders { get; set; }
        public IList<User> Users { get; set; }
    }
}
