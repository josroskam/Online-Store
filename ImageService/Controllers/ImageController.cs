using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ImageService.Services;
using System.Threading.Tasks;

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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] string blobId, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("Invalid image file.");

            var blobUrl = await _imageService.UploadImageAsync(blobId, image);
            return Ok(new { BlobUrl = blobUrl });
        }

        [HttpGet("{blobId}")]
        public async Task<IActionResult> GetImage(string blobId)
        {
            try
            {
                var imageData = await _imageService.GetImageAsync(blobId);
                return File(imageData, "application/octet-stream");
            }
            catch
            {
                return NotFound($"Image with ID {blobId} not found.");
            }
        }

        [HttpDelete("{blobId}")]
        public async Task<IActionResult> DeleteImage(string blobId)
        {
            await _imageService.DeleteImageAsync(blobId);
            return NoContent();
        }
    }
}
