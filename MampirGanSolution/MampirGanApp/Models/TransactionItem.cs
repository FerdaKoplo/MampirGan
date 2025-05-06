using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Models
{
    public class TransactionItem
    {
        public int TransactionItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public List<Product> Products { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}
