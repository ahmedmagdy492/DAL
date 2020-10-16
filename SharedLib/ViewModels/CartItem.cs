using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLib.ViewModels
{
    public class CartItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public double Total { get; set; }
    }
}
