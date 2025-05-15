using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Models
{
    public class OrderHistory
    {
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public string Item { get; set; }
        public int Quantity { get; set; }
        public string Role { get; set; } // "Admin" or "Customer"
    }
}
