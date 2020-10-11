using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order : BaseModel
    {
        public DateTime OrderDate { get; set; }

        [Column(Order = 0)]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; }
        public double TotalInvoiceValue { get; set; }

        [Column(Order = 1)]
        [ForeignKey(nameof(Branch))]
        public int BranchId { get; set; }
        public Branch Branch { get; set; }

        public IList<OrderItems> OrderItems { get; set; }
    }
}
