using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MampirGanApp.Models;

namespace MampirGanApp.Views
{
    public class OrderHistoryView
    {
        public void ShowAll(List<OrderHistory> orders)
        {
            Console.WriteLine("\n=== Riwayat Pesanan ===");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.OrderId} - {order.CustomerName} - {order.Item} ({order.Quantity})");
            }
        }

        public void ShowFiltered(List<OrderHistory> orders, string customerName)
        {
            var filtered = orders.Where(o => o.CustomerName == customerName).ToList();
            ShowAll(filtered);
        }
    }
}
