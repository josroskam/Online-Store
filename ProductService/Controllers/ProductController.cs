using Microsoft.AspNetCore.Mvc;
using ProductService.Services;
using ProductService.Models;
using ProductService.Interfaces;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService imageService)
        {
            _productService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = new List<Product>()
            {
                new Product { ProductId = Guid.NewGuid(), Name = "Product 1", Status = "Pending" },
            };
            return Ok(products);
        }
    }
}
