using Microsoft.AspNetCore.Mvc;
using ImageService.Services;
using ImageService.Models;
using Azure.Storage.Blobs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ImageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _imageService;
        private readonly BlobServiceClient _blobServiceClient;

        public ImageController(IImageService imageService, BlobServiceClient blobServiceClient)
        {
            _imageService = imageService;
            _blobServiceClient = blobServiceClient;
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage([FromForm] string blobId, [FromForm] IFormFile image)
        {
            var blobUrl = await _imageService.UploadImageAsync(blobId, image);
            return Ok(blobUrl); // Return the URL or ID of the stored blob
        }

    }
}
