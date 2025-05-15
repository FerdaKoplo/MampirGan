using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MampirGanApp.Models;
using MampirGanApp.Views;
using MampirGanApp.Enums.Events;

namespace MampirGanApp.Controllers
{
    public class OrderHistoryController
    {
        private List<OrderHistory> orderHistories;
        private OrderHistoryView view;

        public OrderHistoryController()
        {
            
            orderHistories = new List<OrderHistory>
            {
                new OrderHistory { OrderId = "ORD001", CustomerName = "Tianto", Item = "Latte", Quantity = 2, Role = "Customer" },
                new OrderHistory { OrderId = "ORD002", CustomerName = "Ivan", Item = "Americano", Quantity = 3, Role = "Admin" },
            };

            view = new OrderHistoryView();
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== Menu Riwayat Pesanan ===");
                Console.WriteLine("1. Lihat Semua");
                Console.WriteLine("2. Filter Berdasarkan Customer");
                Console.WriteLine("0. Keluar");

                Console.Write("Pilih: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        view.ShowAll(orderHistories);
                        break;
                    case "2":
                        Console.Write("Masukkan nama customer: ");
                        var name = Console.ReadLine();
                        view.ShowFiltered(orderHistories, name);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Input tidak valid");
                        break;
                }
            }
        }
    }
}

