using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using OrderMetricsCalculationFunction.Models;
using OrderMetricsCalculationFunction.Repository;
using OrderService.Models;

namespace OrderMetricsCalculationFunction.Services
{
    public class OrderMetricsService : IOrderMetricsService
    {
        private readonly HttpClient _httpClient;
        private readonly IOrderMetricsRepository _repository;

        public OrderMetricsService(HttpClient httpClient, IOrderMetricsRepository repository)
        {
            _httpClient = httpClient;
            _repository = repository;
        }

        public async Task CalculateAndStoreMetricsAsync()
        {
            int totalOrders = 0;
            int processedOrders = 0;
            TimeSpan totalShippingTime = TimeSpan.Zero;

            int shippedOrdersCount = 0;

            int page = 1;
            const int pageSize = 100;
            bool hasMoreData;

            do
            {
                var response = await _httpClient.GetAsync($"https://orderservice/api/orders?page={page}&pageSize={pageSize}");
                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                var orders = JsonSerializer.Deserialize<List<Order>>(await response.Content.ReadAsStringAsync());
                hasMoreData = orders.Count > 0;

                foreach (var order in orders)
                {
                    totalOrders++;
                    if (order.Status == OrderStatus.Processing || order.Status == OrderStatus.Shipped)
                    {
                        processedOrders++;
                    }

                    if (order.Status == OrderStatus.Shipped && order.ShippingDate.HasValue)
                    {
                        shippedOrdersCount++;
                        totalShippingTime += order.ShippingDate.Value - order.OrderDate;
                    }
                }

                page++;
            } while (hasMoreData);

            var averageShippingTime = shippedOrdersCount > 0
                ? totalShippingTime / shippedOrdersCount
                : TimeSpan.Zero;

            var metrics = new OrderMetrics
            {
                PartitionKey = "Metrics",
                RowKey = Guid.NewGuid().ToString(),
                TotalOrders = totalOrders,
                ProcessedOrders = processedOrders,
                AverageProcessingTime = averageShippingTime.ToString(@"dd\.hh\:mm\:ss"),
                Timestamp = DateTime.UtcNow
            };

            await _repository.SaveMetricsAsync(metrics);
        }
    }
}
