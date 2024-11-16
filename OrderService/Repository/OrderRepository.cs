using Microsoft.Azure.Cosmos;
using OrderService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderService.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly Container _container;

        public OrderRepository(string connectionString, string databaseName, string containerName)
        {
            var cosmosClient = new CosmosClient(connectionString);
            _container = cosmosClient.GetContainer(databaseName, containerName);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _container.CreateItemAsync(order, new PartitionKey(order.Id.ToString()));
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var query = _container.GetItemQueryIterator<Order>("SELECT * FROM c");
            var results = new List<Order>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<IEnumerable<Order>> GetOrdersByPageAsync(int page, int pageSize)
        {
            var query = _container.GetItemQueryIterator<Order>(
                $"SELECT * FROM c OFFSET {page * pageSize} LIMIT {pageSize}"
            );
            var results = new List<Order>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<Order> GetOrderByIdAsync(Guid id)
        {
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
            await _container.UpsertItemAsync(order, new PartitionKey(order.Id.ToString()));
        }
    }
}
