using MampirGanApp.Seeder;
using MampirGanApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Controllers
{
    public class MenuUserController
    {
        private bool running = true;
        private readonly MenuUserView view = new();
        private readonly CategoryController category = new();
        private readonly CartView viewCart = new();
        public void ViewMenuUser()
        {
            ProductSeeder.seedProducts();
            CategorySeeder.seedCategories();

            running = true;
            while (running)
            {
                view.ShowMenu();
                var input = Console.ReadLine();

                switch (input)
                {
                    case "3":
                        category.Run();
                        break;
                    case "4":
                        viewCart.Show();
                        break;
                    case "0":
                        running = false;
                        break;
                    default:
                        view.ShowInvalidInput();
                        break;
                }
            }
        }
    }
}
