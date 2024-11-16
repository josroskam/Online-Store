using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ImageService.Services
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(string blobId, IFormFile image);
        Task<byte[]> GetImageAsync(string blobId);
        Task DeleteImageAsync(string blobId);
    }
}
