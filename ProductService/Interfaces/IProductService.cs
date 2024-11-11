using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductService 
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid productId);
        Task UpdateProductStatusAsync(Guid productId, string status);
    }
}
