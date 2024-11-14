using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> CreateProductAsync(Product product, Stream imageStream, CancellationToken cancellationToken = default);
        Task UpdateProductAsync(Guid id, Product product);

    }
}
