using MampirGanApp.Controllers;

using MampirGanApp.Models;
using MampirGanApp.Views;
using MampirGanApp.Services;

class Program
{
    static void Main()
    {
        AuthService authService = new AuthService();
        AuthView authView = new AuthView();

        while (true)
        {
            Console.WriteLine("1. Register");
            Console.WriteLine("2. Login");
            Console.WriteLine("0. Exit");
            Console.Write("Pilih menu: ");
            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    var (email, username, password) = authView.GetRegisterInput();
                    bool success = authService.Register(email, username, password);
                    authView.ShowRegisterResult(success);
                    break;

                case "2":
                    var (userOrEmail, pass) = authView.GetLoginInput();
                    var user = authService.Login(userOrEmail, pass);
                    authView.ShowLoginResult(user);

                    if (user != null)
                    {
                        if (user.Role == UserRole.Admin)
                        {
                            ShowAdminMenu(); // arahkan ke halaman admin
                        }
                        else
                        {
                            ShowCustomerMenu(); // arahkan ke halaman customer
                        }
                    }
                    break;

                case "0":
                    return;
            }
        }
    }

    static void ShowCustomerMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== CUSTOMER MENU ===");
            Console.WriteLine("1. Lihat list menu");
            Console.WriteLine("2. Pencarian menu");
            Console.WriteLine("3. Keranjang");
            Console.WriteLine("4. Checkout");
            Console.WriteLine("5. History transaksi");
            Console.WriteLine("0. Logout");
            Console.Write("Pilih menu: ");
            string input = Console.ReadLine();

            if (input == "0") break;

            // Panggil method dari view atau controller sesuai input
        }
    }

    static void ShowAdminMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== ADMIN MENU ===");
            Console.WriteLine("1. Manage list menu");
            Console.WriteLine("2. Melihat history transaksi customer");
            Console.WriteLine("0. Logout");
            Console.Write("Pilih menu: ");
            string input = Console.ReadLine();

            if (input == "0") break;

            
        }
    }
}
