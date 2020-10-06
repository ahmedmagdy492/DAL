using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OrderItems : BaseModel
    {
        [ForeignKey(nameof(Order))]
        [Column(Order = 0)]
        public int OrderId { get; set; }

        [ForeignKey(nameof(Product))]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
