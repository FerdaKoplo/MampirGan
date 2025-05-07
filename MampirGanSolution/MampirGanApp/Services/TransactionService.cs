using MampirGanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Services
{
    public class TransactionService
    {
        public static List<Transaction> transactions = new();

        public void ProcessCheckout(List<Cart> cartItems)
        {
            if(cartItems == null || cartItems.Count == 0)
            {
                Console.WriteLine("Keranjang kosong. Tidak bisa checkout.");
                return;
            }

            var transaction = new Transaction
            {
                TransactionID = new Random().Next(1000, 9999),
                Date = DateTime.Now,
                TotalAmount = cartItems.Sum(i => i.Quantity * i.products.Price),
                Items = new List<TransactionItem>()
            };

            foreach (var item in cartItems)
            {
                transaction.Items.Add(new TransactionItem
                {
                    ProductID = item.ProductID,
                    ProductName = item.products.ProductName,
                    Quantity = item.Quantity,
                    SubTotal = item.Quantity * item.products.Price
                });
            }

            transactions.Add(transaction);

            Console.WriteLine($"Transaksi berhasil! ID: {transaction.TransactionID}");
            Console.WriteLine($"Tanggal: {transaction.Date}");
            Console.WriteLine("Detail:");
            foreach (var item in transaction.Items)
            {
                Console.WriteLine($"- {item.ProductName} x{item.Quantity} = {item.SubTotal:C}");
            }
            Console.WriteLine($"Total: {transaction.TotalAmount:C}");
        }

    }
}
