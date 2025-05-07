using MampirGanApp.Enums.Events;
using MampirGanApp.Seeder;
using MampirGanApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MampirGanApp.Controllers
{
    public class CategoryRule
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public CategoryEvent Event { get; set; }
        public Action<CategoryController> Action { get; set; }

        public CategoryRule(string key, string label, CategoryEvent @event, Action<CategoryController> action)
        {
            Key = key;
            Label = label;
            Event = @event;
            Action = action;
        }
    }
    public class CategoryController
    {
        private readonly CategoryService service = new();
        private readonly List<CategoryRule> categoryCommand;

        public CategoryController()
        {
            categoryCommand = new List<CategoryRule>
            {
                new("1", "Lihat Semua Kategori", CategoryEvent.ViewAll, ctrl => ctrl.ViewAll()),
                new("2", "Lihat Produk Berdasarkan Kategori", CategoryEvent.ViewProductsByCategory, ctrl => ctrl.ViewProductsByCategory()),
                new("0", "Keluar", CategoryEvent.Exit, ctrl => ctrl.Exit())
            };
        }

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("\n=== Menu Kategori ===");
                foreach (var cmd in categoryCommand)
                {
                    Console.WriteLine($"{cmd.Key}. {cmd.Label}");
                }

                Console.Write("Pilih: ");
                var input = Console.ReadLine();

                var command = categoryCommand.Find(c => c.Key == input);
                if (command != null)
                {
                    command.Action(this);
                    if (command.Event == CategoryEvent.Exit) break;
                }
                else
                {
                    Console.WriteLine("Pilihan tidak valid.");
                }
            }
        }

        private void ViewAll() => service.ViewAll();

        private void Exit()
        {
            Console.WriteLine("Keluar dari menu kategori.");
        }

        private void ViewProductsByCategory()
        {
            Console.Write("Masukkan Nama Kategori: ");
            string categoryName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                var category = CategorySeeder.Categories.FirstOrDefault(
                    c => string.Equals(c.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase)
                );

                if (category != null)
                {
                    var products = ProductSeeder.Products.Where(p => p.CategoryID == category.CategoryID).ToList();
                    if (products.Any())
                    {
                        Console.WriteLine($"\nProduk dalam kategori '{category.CategoryName}':");
                        foreach (var p in products)
                        {
                            Console.WriteLine($"- (ID {p.ProductID}) {p.ProductName} (Rp{p.Price}, Stok: {p.Stock})");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tidak ada produk dalam kategori ini.");
                    }
                }
                else
                {
                    Console.WriteLine("Kategori tidak ditemukan.");
                }
            }
            else
            {
                Console.WriteLine("Input tidak valid.");
            }
        }


    }
}
