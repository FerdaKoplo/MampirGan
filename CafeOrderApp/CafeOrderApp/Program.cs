using CafeOrderApp.Models;
using CafeOrderApp.Services;

var orderService = new OrderService();

Console.WriteLine("=== Cafe Order System ===");
Console.Write("Login as (Admin/Customer): ");
string role = Console.ReadLine()?.Trim().ToLower() ?? "";

if (role == "admin")
{
    bool running = true;
    while (running)
    {
        Console.WriteLine("\n[ADMIN MENU]");
        Console.WriteLine("1. View Orders");
        Console.WriteLine("2. Add Order");
        Console.WriteLine("3. Delete Order");
        Console.WriteLine("4. Exit");
        Console.Write("Choose: ");
        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                var allOrders = await orderService.GetOrdersAsync();
                PrintOrders(allOrders);
                break;
            case "2":
                Console.Write("Customer Name: ");
                string? name = Console.ReadLine();
                Console.Write("Item: ");
                string? item = Console.ReadLine();
                Console.Write("Price: ");
                double price = double.Parse(Console.ReadLine() ?? "0");

                await orderService.AddOrderAsync(new Order
                {
                    CustomerName = name!,
                    Item = item!,
                    Price = price,
                    Date = DateTime.Now
                });
                Console.WriteLine("Order added.");
                break;
            case "3":
                Console.Write("Enter Order ID to delete: ");
                int orderId = int.Parse(Console.ReadLine() ?? "0");
                await orderService.DeleteOrderAsync(orderId);
                Console.WriteLine("Order deleted.");
                break;
            case "4":
                running = false;
                break;
        }
    }
}
else if (role == "customer")
{
    Console.Write("Enter your name: ");
    string? customerName = Console.ReadLine();

    var orders = await orderService.GetOrdersAsync();
    var yourOrders = orders.Where(o => o.CustomerName.Equals(customerName, StringComparison.OrdinalIgnoreCase)).ToList();

    Console.WriteLine($"\n[ORDER HISTORY for {customerName}]");
    PrintOrders(yourOrders);
}
else
{
    Console.WriteLine("Unknown role.");
}

static void PrintOrders(List<Order> orders)
{
    if (!orders.Any())
    {
        Console.WriteLine("No orders found.");
        return;
    }

    foreach (var o in orders)
    {
        Console.WriteLine($"ID: {o.OrderId} | {o.CustomerName} ordered {o.Item} for Rp{o.Price} on {o.Date:g}");
    }
}

