using CafeOrderApp.Models;
using CafeOrderApp.Repositories;

namespace CafeOrderApp.Services
{
    public class OrderService
    {
        private readonly GenericJsonRepository<Order> _repo;

        public OrderService()
        {
            _repo = new GenericJsonRepository<Order>("orders.json");
        }

        public async Task<List<Order>> GetOrdersAsync() => await _repo.GetAllAsync();

        public async Task AddOrderAsync(Order order)
        {
            var orders = await _repo.GetAllAsync();
            order.OrderId = orders.Count > 0 ? orders.Max(o => o.OrderId) + 1 : 1;
            orders.Add(order);
            await _repo.SaveAllAsync(orders);
        }

        public async Task DeleteOrderAsync(int id)
        {
            var orders = await _repo.GetAllAsync();
            var target = orders.FirstOrDefault(o => o.OrderId == id);
            if (target != null)
            {
                orders.Remove(target);
                await _repo.SaveAllAsync(orders);
            }
        }
    }
}


