using MampirGanApp.Controllers;
using MampirGanApp.Seeder;
using MampirGanApp.Views;

class Program
{
    static void Main(string[] args)
    {
        var MenuUser = new MenuUserController();
        MenuUser.ViewMenuUser();

        var orderHistoryCtrl = new OrderHistoryController();
        orderHistoryCtrl.Run();

    }
}