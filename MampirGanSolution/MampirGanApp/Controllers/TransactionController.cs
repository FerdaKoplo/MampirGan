using MampirGanApp.Enums.Events;
using MampirGanApp.Enums.States;
using MampirGanApp.Models;
using MampirGanApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace MampirGanApp.Controllers
{
    public class TransactionRule
    {
        public string Key { get; }
        public string Label { get; }
        public CheckoutEvent Event { get; }
        public CheckoutState[] AllowedStates { get; }
        public Action<TransactionController> Action { get; }

        public TransactionRule(string key, string label, CheckoutEvent ev, CheckoutState[] allowed, Action<TransactionController> action)
        {
            Key = key;
            Label = label;
            Event = ev;
            AllowedStates = allowed;
            Action = action;
        }
    }
    public class TransactionController
    {
        private CheckoutState state = CheckoutState.Idle;
        private readonly TransactionService service = new();
        private readonly List<Cart> currentCart;
        private static readonly Dictionary<(CheckoutState, CheckoutEvent), CheckoutState> Transitions = new()
        {
            { (CheckoutState.Idle, CheckoutEvent.ViewCart), CheckoutState.ViewingCart },
            { (CheckoutState.ViewingCart, CheckoutEvent.ConfirmCheckout), CheckoutState.Confirming },
            { (CheckoutState.Confirming, CheckoutEvent.Pay), CheckoutState.Processing },
            { (CheckoutState.Processing, CheckoutEvent.Success), CheckoutState.Completed },
            { (CheckoutState.Completed, CheckoutEvent.Exit), CheckoutState.Completed }
        };

        private readonly List<TransactionRule> transactionCommands;
        public TransactionController(List<Cart> cart)
        {
            currentCart = cart;

            transactionCommands = new List<TransactionRule>
        {
            new("1", "Lihat Keranjang", CheckoutEvent.ViewCart, new[]{ CheckoutState.Idle }, ctrl => ctrl.ViewCart()),
            new("2", "Konfirmasi Checkout", CheckoutEvent.ConfirmCheckout, new[]{ CheckoutState.ViewingCart }, ctrl => ctrl.ConfirmCheckout()),
            new("3", "Bayar", CheckoutEvent.Pay, new[]{ CheckoutState.Confirming }, ctrl => ctrl.Pay()),
            new("4", "Exit", CheckoutEvent.Exit, new[]{ CheckoutState.Completed }, ctrl => ctrl.Exit())
        };
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine($"\n=== Checkout State: {state} ===");
                foreach (var cmd in transactionCommands)
                {
                    if (cmd.AllowedStates.Contains(state))
                        Console.WriteLine($"{cmd.Key}. {cmd.Label}");
                }

                Console.Write("Pilih: ");
                var input = Console.ReadLine();
                var rule = transactionCommands.Find(r => r.Key == input);

                if (rule == null || !rule.AllowedStates.Contains(state))
                {
                    Console.WriteLine("Pilihan tidak valid.");
                    continue;
                }

                rule.Action(this);

                if (Transitions.TryGetValue((state, rule.Event), out var next))
                    state = next;

                if (rule.Event == CheckoutEvent.Exit)
                    running = false;
            }
        }


        private void ViewCart()
        {
            if (currentCart.Count == 0)
            {
                Console.WriteLine("Keranjang Kosong");
            }
            else
            {
                foreach (var item in currentCart)
                {
                    Console.WriteLine($"{item.products.ProductName} x{item.Quantity}");
                }
            }
        }

        private void ConfirmCheckout()
        {
            Console.WriteLine("Checkout dikonfirmasi.");
        }

        private void Pay()
        {
            Console.WriteLine("Memproses pembayaran...");
            service.ProcessCheckout(currentCart);
            if (Transitions.TryGetValue((state, CheckoutEvent.Success), out var successState))
                state = successState;
        }

        private void Exit()
        {
            Console.WriteLine("Keluar dari proses checkout.");
        }

    }
}
