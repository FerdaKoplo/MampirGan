using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public decimal TotalAmount { get; set; }
        public List<TransactionItem> Items { get; set; } = new List<TransactionItem>();
    }
}
