using ProductService.Models;

namespace ProductService.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product> CreateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductAsync(Guid id, Product product)
        {
            throw new NotImplementedException();
        }
    }
}
