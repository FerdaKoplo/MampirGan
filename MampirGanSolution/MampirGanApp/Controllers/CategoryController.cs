using MampirGanApp.Enums.Events;
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
                new("2", "Cari Kategori Berdasarkan ID", CategoryEvent.FindById, ctrl => ctrl.FindById()),
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

        private void FindById()
        {
            Console.Write("Masukkan ID Kategori: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                service.FindById(id);
            }
            else
            {
                Console.WriteLine("Input tidak valid.");
            }
        }

        private void Exit()
        {
            Console.WriteLine("Keluar dari menu kategori.");
        }

    }
}
