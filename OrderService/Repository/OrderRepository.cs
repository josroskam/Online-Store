using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public Task<Order> CreateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderStatusAsync(Guid id, Order order)
        {
            throw new NotImplementedException();
        }
    }
}