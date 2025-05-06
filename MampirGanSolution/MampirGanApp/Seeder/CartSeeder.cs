using MampirGanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Seeder
{
    public class CartSeeder
    {
        public static List<Cart> SeedCart()
        {
            return new List<Cart>
            {
                new Cart
                {
                    ProductID = 1,
                    Quantity = 2,
                    products = new Product
                    {
                        ProductID = 1,
                        ProductName = "Kopi Hitam",
                        Price = 15000
                    }
                },
                new Cart
                {
                    ProductID = 2,
                    Quantity = 1,
                    products = new Product
                    {
                        ProductID = 2,
                        ProductName = "Roti Bakar",
                        Price = 20000
                    }
                }
            };
        }
    }
}
