using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public User users { get; set; }
        public int ProductID { get; set; }
        public Product products { get; set; }
        public int Quantity { get; set; }
    }
}
