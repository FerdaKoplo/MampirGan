using MampirGanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Seeder
{
    public class ProductSeeder
    {
        public static List<Product> Products = new List<Product>();
        public static void seedProducts()
        {
            var products = new List<Product>
        {
            new Product { ProductID = 1, ProductName = "Americano", Price = 15000, Stock = 10, CategoryID = 1 },
            new Product { ProductID = 2, ProductName = "Cappuccino", Price = 17000, Stock = 15, CategoryID = 1 },
            new Product { ProductID = 3, ProductName = "Iced Latte", Price = 18000, Stock = 20, CategoryID = 2 }
        };
        }
    }
}
