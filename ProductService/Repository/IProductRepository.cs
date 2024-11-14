using ProductService.Models;

namespace ProductService.Repository
{
    public interface IProductRepository
    {
        Task<Product> CreateProductAsync(Product product);
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task UpdateProductAsync(Guid id, Product product);
    }
}
