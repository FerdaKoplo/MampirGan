using MampirGanApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Seeder
{
    public class CategorySeeder
    {
        public static void seedCategories()
        {
            var categories = new List<Category>
        {
            new Category { CategoryID = 1, CategoryName = "Kopi Panas", Description = "Segala jenis kopi panas" },
            new Category { CategoryID = 2, CategoryName = "Es Kopi", Description = "Kopi dingin dan segar" },
            new Category { CategoryID = 3, CategoryName = "Non-Kopi", Description = "Minuman selain kopi" }
        };
        }
    }
}
