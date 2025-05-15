using MampirGan_Automata__dan_Table_driven_.Enums.States;
using MampirGan_Automata__dan_Table_driven_.Objects;
using MampirGan_Automata__dan_Table_driven_.Utils;
using MampirGanApp.Enums.Events;
using MampirGanApp.Enums.States;

namespace MampirGan_Automata__dan_Table_driven_.Fitur
{
    public class CategoryRule
    {
        public string Key { get; set; }
        public string Label { get; set; }
        public CategoryEvent Event { get; set; }

        public CategoryState[] AllowedStates { get; set; }
        public Action<CategoryLogic> Action { get; set; }

        public CategoryRule(string key, string label, CategoryEvent ev, CategoryState[] allowed, Action<CategoryLogic> action)
        {
            Key = key;
            Label = label;
            Event = ev;
            AllowedStates = allowed;
            Action = action;
        }
    }
    public class CategoryLogic
    {
        private CategoryState state = CategoryState.Initial;
        private readonly CategoryService service = new();
        private readonly List<CategoryRule> categoryCommand;

        private static readonly Dictionary<(CategoryState, CategoryEvent), CategoryState> Transitions = new()
        {
            { (CategoryState.Initial, CategoryEvent.ViewAll), CategoryState.Viewing},
            { (CategoryState.Initial, CategoryEvent.ViewProductsByCategory), CategoryState.Viewing},
            { (CategoryState.Viewing, CategoryEvent.Exit), CategoryState.Exited},
            { (CategoryState.ViewingByCategory, CategoryEvent.Exit), CategoryState.Exited},
        };

        public CategoryLogic()
        {
           categoryCommand = new List<CategoryRule>()
            {
                new("1", "Lihat Semua Kategori", CategoryEvent.ViewAll, new[]{ CategoryState.Initial }, ctrl => ctrl.ViewAll()),
                new("2", "Lihat Produk Berdasarkan Kategori", CategoryEvent.ViewProductsByCategory, new[] { CategoryState.Viewing }, ctrl => ctrl.ViewProductsByCategory()),
                new("0", "Keluar", CategoryEvent.Exit, new[] { CategoryState.Initial, CategoryState.Viewing, CategoryState.ViewingByCategory}, ctrl => ctrl.Exit())
            };
        }

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine($"\n=== Keranjang Action: {state} ===");
                foreach (var cmd in categoryCommand)
                {
                    if (Array.Exists(cmd.AllowedStates, s => s == state))
                        Console.WriteLine($"{cmd.Key}. {cmd.Label}");
                }

                Console.Write("Pilih: ");
                var choice = Console.ReadLine();
                var rule = categoryCommand.Find(c => c.Key == choice);

                if (rule == null || !rule.AllowedStates.Contains(state))
                {
                    Console.WriteLine("Pilih yang ada pada menu.");
                    continue;
                }
                rule.Action(this);

                if (Transitions.TryGetValue((state, rule.Event), out var next))
                    state = next;
            }
        }

        private void ViewAll() => service.ViewAll();

        private void Exit()
        {
            Console.WriteLine("Keluar dari menu kategori.");
        }

        private void ViewProductsByCategory() => service.ViewProductsByCategory();
    }

    public class CategoryService
    {
        private readonly List<Category> categories = new();
        private readonly string categoryDataPath = "C:\\Users\\IVAN\\Documents\\Project_C#\\MampirGan-Automata-Table-driven-only-\\MampirGan(Automata, dan Table driven)\\MampirGan(Automata, dan Table driven)\\Jsons\\CategoryDummy.json";
        public CategoryService()
        {
            categories = Jsonloader.LoadCategoryFromFile("C:\\Users\\IVAN\\Documents\\Project_C#\\MampirGan-Automata-Table-driven-only-\\MampirGan(Automata, dan Table driven)\\MampirGan(Automata, dan Table driven)\\Jsons\\CategoryDummy.json");
            var loadedCategory = Jsonloader.LoadCartFromFile(categoryDataPath);
            if (categories != null && categories.Any())
            {
                Console.WriteLine("Data kategori berhasil dimuat dari file.");
            }
            else
            {
                Console.WriteLine("Tidak ada data kategori yang tersedia.");
            }
        }
        public void ViewAll()
        {
            Console.WriteLine("=== Daftar Kategori ===");

            if (categories == null)
                throw new InvalidOperationException("Kategori tidak tersedia.");

            foreach (var category in categories)
            {
                Console.WriteLine($"{category.CategoryID}. {category.CategoryName} - {category.Description}");
            }
        }

        public void ViewProductsByCategory()
        {
            Console.Write("Masukkan Nama Kategori: ");
            string categoryName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                string categoryDataPath = "C:\\Users\\IVAN\\Documents\\Project_C#\\MampirGan-Automata-Table-driven-only-\\MampirGan(Automata, dan Table driven)\\MampirGan(Automata, dan Table driven)\\Jsons\\CategoryDummy.json";
                string productDataPath = "C:\\Users\\IVAN\\Documents\\Project_C#\\MampirGan-Automata-Table-driven-only-\\MampirGan(Automata, dan Table driven)\\MampirGan(Automata, dan Table driven)\\Jsons\\ProductDummy.json";

                var categories = Jsonloader.LoadCategoryFromFile(categoryDataPath);
                var products = Jsonloader.LoadProducts(productDataPath);

                if (categories == null || categories.Count == 0)
                    throw new InvalidOperationException("Data kategori tidak tersedia.");

                if (products == null || products.Count == 0)
                    throw new InvalidOperationException("Data produk tidak tersedia.");

                var category = categories.FirstOrDefault(
                    c => string.Equals(c.CategoryName, categoryName, StringComparison.OrdinalIgnoreCase)
                );

                if (category == null)
                    throw new InvalidOperationException("Kategori tidak ditemukan.");

                if (category != null)
                {
                    var filteredProducts = products.Where(p => p.CategoryID == category.CategoryID).ToList();
                    if (filteredProducts.Any())
                    {
                        Console.WriteLine($"\nProduk dalam kategori '{category.CategoryName}':");
                        foreach (var p in filteredProducts)
                        {
                            Console.WriteLine($"- (ID : {p.ProductID}) {p.ProductName} (Rp{p.Price}, Stok: {p.Stock})");
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
