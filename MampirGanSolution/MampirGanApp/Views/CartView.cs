using MampirGanApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Views
{
    public class CartView
    {
        private readonly CartController controller;

        public CartView()
        {
            controller = new CartController();
        }

        public void Show()
        {
            controller.Run();
        }
    }
}
