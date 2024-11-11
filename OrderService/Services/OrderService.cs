using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        public OrderService() { }

        public Task<Order> CreateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderStatusAsync(Guid orderId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
