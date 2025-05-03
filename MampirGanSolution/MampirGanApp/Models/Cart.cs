using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Models
{
    public class Cart
    {
        public int cartID { get; set; }
        public int UserID { get; set; }
        public User users;
        public int ProductID { get; set; }
        public Product products;
        public int Quantity { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
