using Microsoft.Azure.Cosmos;
using ReviewService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewService.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public ReviewRepository(string connectionString, string databaseName, string containerName)
        {
            _cosmosClient = new CosmosClient(connectionString);
            var database = _cosmosClient.GetDatabase(databaseName);
            _container = database.GetContainer(containerName);
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            review.Id = Guid.NewGuid();
            await _container.CreateItemAsync(review, new PartitionKey(review.ProductId));
            return review;
        }

        public async Task<Review> GetReviewByIdAsync(Guid id)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id")
                .WithParameter("@id", id.ToString());

            var iterator = _container.GetItemQueryIterator<Review>(query);
            var results = await iterator.ReadNextAsync();

            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(string productId)
        {
            var query = new QueryDefinition("SELECT * FROM c WHERE c.ProductId = @productId")
                .WithParameter("@productId", productId);

            var iterator = _container.GetItemQueryIterator<Review>(query);
            var results = new List<Review>();

            while (iterator.HasMoreResults)
            {
                var response = await iterator.ReadNextAsync();
                results.AddRange(response);
            }

            return results;
        }

        public async Task UpdateReviewAsync(Guid id, Review review)
        {
            var existingReview = await GetReviewByIdAsync(id);
            if (existingReview == null)
            {
                throw new InvalidOperationException($"Review with ID {id} not found.");
            }

            review.Id = id;  // Ensure the ID remains unchanged
            await _container.UpsertItemAsync(review, new PartitionKey(review.ProductId));
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            var review = await GetReviewByIdAsync(id);
            if (review != null)
            {
                await _container.DeleteItemAsync<Review>(id.ToString(), new PartitionKey(review.ProductId));
            }
        }
    }
}
