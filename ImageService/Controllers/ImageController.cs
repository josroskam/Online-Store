using Microsoft.AspNetCore.Mvc;
using ImageService.Services;
using ImageService.Models;

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
                new Image { Id = Guid.NewGuid(), FileName = "Image 1", Data = new byte[1] },
            };
            return Ok(images);
        }
    }
}
