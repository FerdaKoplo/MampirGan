using MampirGanApp.Seeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Services
{
    public class CategoryService
    {
        public void ViewAll()
        {
            var categories = CategorySeeder.Categories;
            Console.WriteLine("=== Daftar Kategori ===");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryID}. {category.CategoryName} - {category.Description}");
            }
        }

        public void FindById(int id)
        {
            var category = CategorySeeder.Categories.FirstOrDefault(c => c.CategoryID == id);
            if (category == null)
            {
                Console.WriteLine("Kategori tidak ditemukan.");
            }
            else
            {
                Console.WriteLine($"ID: {category.CategoryID} Nama: {category.CategoryName} Deskripsi: {category.Description}");
            }
        }
    }
}
