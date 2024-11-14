using Microsoft.AspNetCore.Mvc;
using ProductService.Models;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
    }
}
