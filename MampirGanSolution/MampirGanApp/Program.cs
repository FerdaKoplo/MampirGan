using MampirGanApp.Controllers;
using MampirGanApp.Models;
using MampirGanApp.Views;
using MampirGanApp.Services;

class Program
{
    // Table-driven storage
    static List<MenuItem> menuList = new List<MenuItem>();


    public enum MenuCategory
    {
        Kopi,
        NonKopi,
        Snack
    }

    //model
    public class MenuItem
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public double Harga { get; set; }
        public MenuCategory Kategori { get; set; }
    }

    static void Main()
    {
        AuthService authService = new AuthService();
        AuthView authView = new AuthView();
        AuthController authController = new AuthController();

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
                    authController.Register(); 
                    break;

                case "2":
                    var user = authController.Login(); 
                    if (user != null)
                    {
                        if (user.Role == UserRole.Admin)
                            ShowAdminMenu();
                        else
                            ShowCustomerMenu();
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

            // DBC: precondition
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input tidak boleh kosong.");
            if (!new[] { "0", "1", "2", "3", "4", "5" }.Contains(input))
                throw new ArgumentOutOfRangeException("Pilihan menu tidak valid.");

            switch (input)
            {
                case "1":
                    foreach (var menu in menuList)
                    {
                        Console.WriteLine($"[{menu.Id}] {menu.Nama} - Rp{menu.Harga}");
                    }
                    break;
                case "2": break;
                case "3": break;
                case "4": break;
                case "5": break;
                case "0":
                    Console.WriteLine("Logout berhasil.");
                    return;
            }
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

            // DBC: precondition
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Input tidak boleh kosong.");
            if (!new[] { "0", "1", "2" }.Contains(input))
                throw new ArgumentOutOfRangeException("Pilihan menu tidak valid.");

            switch (input)
            {
                case "1":
                    ManageMenu(); 
                    break;
                case "2":
                    
                    break;
                case "0":
                    Console.WriteLine("Logout berhasil.");
                    return;
            }
        }
    }

   //AUTOMATA: Sub-state machine untuk Manage Menu 
    static void ManageMenu()
    {
        string currentState = "MenuUtama";

        while (true)
        {
            switch (currentState)
            {
                case "MenuUtama":
                    Console.WriteLine("\n== MANAGE MENU ==");
                    Console.WriteLine("1. Tambah Menu");
                    Console.WriteLine("2. Lihat Menu");
                    Console.WriteLine("3. Edit Menu");
                    Console.WriteLine("4. Hapus Menu");
                    Console.WriteLine("0. Kembali");
                    Console.Write("Pilih: ");
                    string pilihan = Console.ReadLine();

                    // DBC
                    if (string.IsNullOrWhiteSpace(pilihan))
                        throw new ArgumentException("Input tidak boleh kosong.");
                    if (!new[] { "0", "1", "2", "3", "4" }.Contains(pilihan))
                        throw new ArgumentOutOfRangeException("Pilihan tidak valid.");

                    switch (pilihan)
                    {
                        case "1": currentState = "Tambah"; break;
                        case "2": currentState = "Lihat"; break;
                        case "3": currentState = "Edit"; break;
                        case "4": currentState = "Hapus"; break;
                        case "0": return;
                    }
                    break;

                case "Tambah":
                    TambahMenu();
                    currentState = "MenuUtama";
                    break;

                case "Lihat":
                    TampilkanMenu();
                    currentState = "MenuUtama";
                    break;

                case "Edit":
                    EditMenu();
                    currentState = "MenuUtama";
                    break;

                case "Hapus":
                    HapusMenu();
                    currentState = "MenuUtama";
                    break;
            }
        }
    }

    static void TambahMenu()
    {
        Console.Write("Nama menu: ");
        string nama = Console.ReadLine();

        Console.Write("Harga: ");
        if (!double.TryParse(Console.ReadLine(), out double harga))
            throw new ArgumentException("Harga harus angka.");

        Console.WriteLine("Kategori (0=Kopi, 1=NonKopi, 2=Snack): ");
        if (!int.TryParse(Console.ReadLine(), out int kategoriIndex) || !Enum.IsDefined(typeof(MenuCategory), kategoriIndex))
            throw new ArgumentException("Kategori tidak valid!");

        var menu = new MenuItem
        {
            Id = menuList.Count + 1,
            Nama = nama,
            Harga = harga,
            Kategori = (MenuCategory)kategoriIndex
        };

        menuList.Add(menu);

        if (!menuList.Contains(menu))
            throw new InvalidOperationException("Menu gagal ditambahkan.");

        Console.WriteLine("Menu berhasil ditambahkan.");
    }


    static void TampilkanMenu()
    {
        if (menuList.Count == 0)
        {
            Console.WriteLine("Belum ada menu.");
            return;
        }

        foreach (var menu in menuList)
        {
            Console.WriteLine($"[{menu.Id}] {menu.Nama} - Rp{menu.Harga}");
        }
    }

    static void EditMenu()
    {
        TampilkanMenu();
        Console.Write("Masukkan ID menu yang ingin diedit: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID tidak valid.");
            return;
        }

        var menu = menuList.FirstOrDefault(m => m.Id == id);
        if (menu == null)
        {
            Console.WriteLine("Menu tidak ditemukan.");
            return;
        }

        Console.Write("Nama baru: ");
        string namaBaru = Console.ReadLine();

        Console.Write("Harga baru: ");
        if (!double.TryParse(Console.ReadLine(), out double hargaBaru))
        {
            Console.WriteLine("Harga tidak valid.");
            return;
        }

        menu.Nama = namaBaru;
        menu.Harga = hargaBaru;
        Console.WriteLine("Menu berhasil diperbarui.");
    }

    static void HapusMenu()
    {
        TampilkanMenu();
        Console.Write("Masukkan ID menu yang ingin dihapus: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("ID tidak valid.");
            return;
        }

        var menu = menuList.FirstOrDefault(m => m.Id == id);
        if (menu == null)
        {
            Console.WriteLine("Menu tidak ditemukan.");
            return;
        }

        menuList.Remove(menu);

        // DBC postcondition
        if (menuList.Any(m => m.Id == id))
            throw new InvalidOperationException("Menu gagal dihapus.");

        Console.WriteLine("Menu berhasil dihapus.");
    }
}
