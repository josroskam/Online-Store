using Microsoft.AspNetCore.Mvc;
using ProductService.Models;
using ProductService.Services;
using System;
using System.Threading;
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] Product product, [FromForm] IFormFile image, CancellationToken cancellationToken)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Image is required.");

            using var imageStream = image.OpenReadStream();
            var createdProduct = await _productService.CreateProductAsync(product, imageStream, cancellationToken);

            return Ok(createdProduct);
        }
    }
}
