using Azure.Storage.Queues;
using Newtonsoft.Json;
using OrderService.Models;
using OrderService.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly QueueClient _queueClient;

        public OrderService(IOrderRepository orderRepository, string queueConnectionString)
        {
            _orderRepository = orderRepository;
            _queueClient = new QueueClient(queueConnectionString, "notifications");
            _queueClient.CreateIfNotExists();
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid();
            order.OrderDate = DateTime.UtcNow;

            // Persist the order to the database
            await _orderRepository.CreateOrderAsync(order);

            // Send a notification message to the queue
            var notificationMessage = new
            {
                RecipientEmail = order.CustomerEmail,
                Subject = "Order Confirmation",
                Message = $"Your order with ID {order.Id} has been successfully created!"
            };

            string messageJson = JsonConvert.SerializeObject(notificationMessage);
            await _queueClient.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(messageJson)));

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            return await _orderRepository.GetOrderByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(int page, int pageSize)
        {
            return await _orderRepository.GetOrdersByPageAsync(page, pageSize);
        }

        public async Task UpdateOrderStatusAsync(Guid id, OrderStatus status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                throw new Exception($"Order with ID {id} not found.");
            }

            order.Status = status;

            if (status == OrderStatus.Shipped)
            {
                order.ShippingDate = DateTime.UtcNow;

                // Send a "Shipped" notification to the queue
                var notificationMessage = new
                {
                    RecipientEmail = order.CustomerEmail,
                    Subject = "Order Shipped",
                    Message = $"Your order with ID {order.Id} has been shipped on {order.ShippingDate}!"
                };

                string messageJson = JsonConvert.SerializeObject(notificationMessage);
                await _queueClient.SendMessageAsync(Convert.ToBase64String(Encoding.UTF8.GetBytes(messageJson)));
            }

            // Update the order in the database
            await _orderRepository.UpdateOrderAsync(order);
        }
    }
}
