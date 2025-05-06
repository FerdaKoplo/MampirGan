using MampirGanApp.Seeder;
using MampirGanApp.Views;

class Program
{
    static void Main(string[] args)
    {
        ProductSeeder.seedProducts();
        var view = new CartView();
        view.Show();
    }
}