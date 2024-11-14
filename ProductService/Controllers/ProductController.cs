using Microsoft.AspNetCore.Mvc;
using ProductService.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProductService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id) =>
            Ok(await _productService.GetProductByIdAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAllProducts() =>
            Ok(await _productService.GetAllProductsAsync());
    }
}
