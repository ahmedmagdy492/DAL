using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Category : BaseModel
    {
        [Required]
        public string Name { get; set; }

        public IList<Product> Products { get; set; }
    }
}
