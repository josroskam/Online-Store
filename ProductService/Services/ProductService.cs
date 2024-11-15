using ProductService.Models;
using ProductService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();
        private readonly HttpClient _httpClient;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _productRepository = productRepository;

            // Add default products for testing
            _products.Add(new Product { Id = Guid.NewGuid(), Name = "Widget A", Price = 10.0m });
        }

        public async Task<Product> GetProductByIdAsync(Guid id) =>
            await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await Task.FromResult(_products);

        public async Task<Product> CreateProductAsync(Product product, Stream imageStream, CancellationToken cancellationToken = default)
        {
            product.Id = Guid.NewGuid();

            // Generate a unique ID for the image blob
            string blobId = Guid.NewGuid().ToString();
            string imageServiceUrl = "https://imageservice/api/upload"; // Endpoint for ImageService

            try
            {
                // Prepare request with image and blobId
                var content = new MultipartFormDataContent
        {
            { new StringContent(blobId), "blobId" },
            { new StreamContent(imageStream), "image", "productImage.jpg" }
        };

                // Send request to ImageService
                var response = await _httpClient.PostAsync(imageServiceUrl, content, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    // Get the Blob URL from the response
                    string blobUrl = await response.Content.ReadAsStringAsync();
                    product.ImageUrl = blobUrl; // Assuming Product model has an ImageUrl property
                }
                else
                {
                    throw new Exception("Failed to upload image to ImageService.");
                }

                // Save the product to the database here, if the image upload succeeds
                await _productRepository.CreateProductAsync(product); 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error uploading image: {ex.Message}");
                throw; // Optionally, rethrow or handle as needed
            }

            return product;
        }


        public async Task UpdateProductAsync(Guid id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Price = product.Price;
                existingProduct.ImageUrl = product.ImageUrl;
            }
            await Task.CompletedTask;
        }
    }
}
