using Microsoft.AspNetCore.Mvc;
using ImageService.Models;

namespace ImageService.Services
{
    public class ImageService : IImageService
    {
        public ImageService() { }
        public Task<Image> CreateImageAsync(Image image)
        {
            throw new NotImplementedException();
        }
        public Task<Image> GetImageByIdAsync(Guid imageId)
        {
            throw new NotImplementedException();
        }
        public Task UpdateImageStatusAsync(Guid imageId, string status)
        {
            throw new NotImplementedException();
        }
    }
}
