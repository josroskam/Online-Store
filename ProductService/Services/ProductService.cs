using ProductService.Interfaces;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        public ProductService() { }
        public Task<Product> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
        public Task<Product> GetProductByIdAsync(Guid productId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateProductStatusAsync(Guid productId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
