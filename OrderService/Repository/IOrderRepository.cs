using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Repository
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<IEnumerable<Order>> GetOrdersByPageAsync(int page, int pageSize);
        Task<Order> GetOrderByIdAsync(Guid id);
        Task UpdateOrderAsync(Order order);
    }
}
