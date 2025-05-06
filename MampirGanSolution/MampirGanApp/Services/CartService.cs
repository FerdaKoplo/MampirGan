using MampirGanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Services
{
    public class CartService
    {
        private readonly List<Cart> CartItems  = new();

        public void AddItem(int productID, int quantity)
        {
            var prod = Seeder.ProductSeeder.Products.Find(p => p.ProductID == productID);
            if (prod == null)
            {
                Console.WriteLine("Item tidak ditemukan");
                return;
            }
            var existingItem = CartItems.FirstOrDefault(c => c.ProductID == productID);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
                Console.WriteLine($"Jumlah Item dirubah dengan nama {prod.ProductName} dengan jumlah {existingItem.Quantity}");
            }
            else
            {
                var cartItem = new Cart()
                {
                    ProductID = productID,
                    Quantity = quantity,
                    products = prod
                };
                CartItems.Add(cartItem);
                Console.WriteLine($"Item {prod.ProductName} dengan jumlah {quantity} telah ditambahkan ke keranjang");
            }
        }
        public void RemoveItem(int productID)
        {
            var existingItem = CartItems.FirstOrDefault(c => c.ProductID == productID);
            if (existingItem == null)
            {
                Console.WriteLine("Item tidak ada di keranjang");
                return;
            }

            CartItems.Remove(existingItem);
            Console.WriteLine($"Item {existingItem.products.ProductName} dihapus dari keranjang");
        }

        public void ViewCart()
        {
            if (!CartItems.Any())
            {
                Console.WriteLine("Keranjang kosong.");
                return;
            }

            Console.WriteLine("=== Isi Keranjang ===");
            foreach (var item in CartItems)
            {
                Console.WriteLine(
                    $"- {item.products.ProductName.PadRight(15)} | Qty: {item.Quantity,3} | " +
                    $"Harga: Rp{item.products.Price:N0} | Subtotal: Rp{(item.products.Price * item.Quantity):N0}");
            }

            var total = CartItems.Sum(i => i.products.Price * i.Quantity);
            Console.WriteLine($"Total belanja: Rp{total:N0}");
        }

        public void ClearCart()
        {
            CartItems.Clear();
            Console.WriteLine("Semua item di keranjang telah dihapus.");
        }
    }
}
