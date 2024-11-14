using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders = new();

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.UtcNow;
            _orders.Add(order);
            return await Task.FromResult(order);
        }

        public async Task<Order> GetOrderByIdAsync(Guid id) =>
            await Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
            await Task.FromResult(_orders);

        public async Task UpdateOrderStatusAsync(Guid id, string status)
        {
            var order = _orders.FirstOrDefault(o => o.Id == id);
            if (order != null)
            {
                order.Status = status;
                if (status == "Shipped") order.ShippingDate = DateTime.UtcNow;
            }
            await Task.CompletedTask;
        }
    }
}
