using ImageService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ImageService.Services
{
    public interface IImageService
    {
        Task<Image> GetImageByIdAsync(Guid id);
        Task<Image> UploadImageAsync(string blobId, [FromForm] IFormFile image);
    }
}
