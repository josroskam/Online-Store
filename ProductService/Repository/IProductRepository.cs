using ProductService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductService.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(Guid id);
        Task UpdateProductAsync(Guid id, Product product);
    }
}
