using Microsoft.AspNetCore.Mvc;
using ImageService.Services;
using ImageService.Models;
using ImageService.Interfaces;

namespace ImageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var images = new List<Image>()
            {
                new Image { Id = Guid.NewGuid(), Name = "Image 1", Status = "Pending" },
            };
            return Ok(images);
        }
    }
}
