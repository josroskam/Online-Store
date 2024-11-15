using Microsoft.Azure.Cosmos;
using ProductService.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public ProductRepository(string connectionString)
        {
            // Initialize the Cosmos Client
            _cosmosClient = new CosmosClient(connectionString);

            // Get or create the database and container
            var database = _cosmosClient.CreateDatabaseIfNotExistsAsync("ProductServiceDB").Result.Database;
            _container = database.CreateContainerIfNotExistsAsync("Products", "/Category", 400).Result.Container;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            product.Id = Guid.NewGuid(); // Assign a new Guid if not already assigned
            var response = await _container.CreateItemAsync(product, new PartitionKey(product.Category));
            return response.Resource;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var query = _container.GetItemQueryIterator<Product>("SELECT * FROM Products");
            var results = new List<Product>();

            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            try
            {
                var response = await _container.ReadItemAsync<Product>(id.ToString(), new PartitionKey("CategoryPlaceholder"));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // Item not found
            }
        }

        public async Task UpdateProductAsync(Guid id, Product product)
        {
            await _container.ReplaceItemAsync(product, id.ToString(), new PartitionKey(product.Category));
        }
    }
}
