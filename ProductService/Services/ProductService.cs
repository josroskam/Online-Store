using ProductService.Interfaces;
using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products = new();

        public ProductService()
        {
            // Add default products for testing
            _products.Add(new Product { Id = Guid.NewGuid(), Name = "Widget A", Price = 10.0m });
        }

        public async Task<Product> GetProductByIdAsync(Guid id) =>
            await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));

        public async Task<IEnumerable<Product>> GetAllProductsAsync() =>
            await Task.FromResult(_products);
    }
}
