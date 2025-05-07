using MampirGanApp.Enums.Events;
using MampirGanApp.Enums.States;
using MampirGanApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Controllers
{
    public class CartRule
    {
        public CartEvent Event { get; }
        public string Key { get; }
        public string Label { get; }
        public CartState[] AllowedStates { get; }
        public Action<CartController> Action{ get;}
        public CartRule(string key, string label, CartEvent ev, CartState[] allowed, Action<CartController> action)
        {
            Key = key;
            Label = label;
            Event = ev;
            AllowedStates = allowed;
            Action = action;
        }
    }

    public class CartController
    {
        private CartState state = CartState.Empty;
        private readonly CartService service = new();
        private TransactionController checkout;

        private static readonly Dictionary<(CartState, CartEvent), CartState> Transitions = new()
    {
        { (CartState.Empty,     CartEvent.AddItem),    CartState.Active    },
        { (CartState.Active,    CartEvent.AddItem),    CartState.Active    },
        { (CartState.Active,    CartEvent.RemoveItem), CartState.Active    },
        { (CartState.Active,    CartEvent.ClearCart),  CartState.Empty     },
        { (CartState.Active,    CartEvent.Checkout),   CartState.CheckedOut},
        { (CartState.CheckedOut, CartEvent.Exit),      CartState.CheckedOut}
    };

        private readonly List<CartRule> cartCommands;

        public CartController()
        {
            cartCommands = new List<CartRule>
        {
            new("1", "Tambahkan Item",    CartEvent.AddItem,    new[]{CartState.Empty, CartState.Active}, ctrl => ctrl.AddItem()),
            new("2", "Hapus Item", CartEvent.RemoveItem, new[]{CartState.Active},              ctrl => ctrl.RemoveItem()),
            new("3", "Lihat Cart",   CartEvent.ViewCart,   new[]{CartState.Empty, CartState.Active}, ctrl => ctrl.ViewCart()),
            new("4", "Bersihkan Keranjang",  CartEvent.ClearCart,  new[]{CartState.Active},              ctrl => ctrl.ClearCart()),
            new("5", "Checkout", CartEvent.Checkout, new[]{CartState.Active}, ctrl => ctrl.Checkout()),
            new("0", "Exit",        CartEvent.Exit,       new[]{CartState.Empty, CartState.Active, CartState.CheckedOut}, ctrl => ctrl.Exit())
        };
        }
        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine($"\n=== Keranjang Action: {state} ===");
                foreach (var cmd in cartCommands)
                {
                    if (Array.Exists(cmd.AllowedStates, s => s == state))
                        Console.WriteLine($"{cmd.Key}. {cmd.Label}");
                }

                Console.Write("Pilih: ");
                var choice = Console.ReadLine();
                var rule = cartCommands.Find(c => c.Key == choice);

                if (rule is null || Array.IndexOf(rule.AllowedStates, state) < 0)
                {
                    Console.WriteLine("Pilih yang ada pada menu.");
                    continue;
                }

                rule.Action(this);

                if (Transitions.TryGetValue((state, rule.Event), out var next))
                    state = next;

                if (rule.Event == CartEvent.Exit)
                    running = false;
            }
        }

        private void AddItem()
        {
            Console.Write("Item yang ingin ditambah: ");
            string productName = Console.ReadLine();

            Console.Write("Jumlah yang ingin dtambah: ");
            if (!int.TryParse(Console.ReadLine(), out int qty)) return;

            service.AddItem(productName, qty);
        }

        private void RemoveItem()
        {
            Console.Write("Item yang ingin dihapus: ");
            if (!int.TryParse(Console.ReadLine(), out int pid)) return;
            service.RemoveItem(pid);
        }

        private void ViewCart() => service.ViewCart();

        private void ClearCart() => service.ClearCart();
        public void Checkout()
        {
            if (!service.HasItems())
            {
                Console.WriteLine("Keranjang kosong, tidak bisa checkout.");
                return;
            }

            service.ViewCart();
            Console.WriteLine("\nLanjutkan ke checkout...");
            checkout = new TransactionController(service.GetCart());
            checkout.Run();

        }

        private void Exit() => Console.WriteLine("Keluar Keranjang.");

    }
}
