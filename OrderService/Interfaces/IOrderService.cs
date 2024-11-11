using OrderService.Models;

namespace OrderService.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(Guid orderId);
        Task UpdateOrderStatusAsync(Guid orderId, string status);
    }
}
