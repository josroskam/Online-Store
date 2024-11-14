using Azure.Storage.Queues;
using Newtonsoft.Json;
using OrderService.Models;
using OrderService.Repository;
using System;
using System.Collections.Generic;
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
            await _queueClient.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(messageJson)));

            return order;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id) =>
            await _orderRepository.GetOrderByIdAsync(id);

        public async Task<IEnumerable<Order>> GetAllOrdersAsync() =>
            await _orderRepository.GetAllOrdersAsync();

        public async Task UpdateOrderStatusAsync(Guid id, string status)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                throw new Exception($"Order with ID {id} not found.");
            }
                         

            // if statement on enum status
            if (order.Status == OrderStatus.Shipped)
            {
                order.ShippingDate = DateTime.UtcNow;

                // Update the order in the database
                await _orderRepository.UpdateOrderAsync(order);

                // Send a "Shipped" notification to the queue
                var notificationMessage = new
                {
                    RecipientEmail = order.CustomerEmail,
                    Subject = "Order Shipped",
                    Message = $"Your order with ID {order.Id} has been shipped on {order.ShippingDate}!"
                };

                string messageJson = JsonConvert.SerializeObject(notificationMessage);
                await _queueClient.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(messageJson)));
            }
        }
    }
}
