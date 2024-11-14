using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using OrderService.Models;

namespace OrderService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public OrderRepository(string connectionString, string databaseName, string containerName)
        {
            _cosmosClient = new CosmosClient(connectionString);
            _container = _cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _container.CreateItemAsync(order, new PartitionKey(order.Id.ToString()));
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            // Query all orders
            var query = _container.GetItemQueryIterator<Order>("SELECT * FROM c");
            var results = new List<Order>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response.ToList());
            }
            return results;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
            // Retrieve a single order by ID
            try
            {
                var response = await _container.ReadItemAsync<Order>(id.ToString(), new PartitionKey(id.ToString()));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            // Update an existing order
            await _container.UpsertItemAsync(order, new PartitionKey(Convert.ToInt32(order.Id)));
        }
    }
}
