using Microsoft.AspNetCore.Mvc;
using ImageService.Models;

namespace ImageService.Interfaces
{
    public interface IImageService 
    {
        Task<Image> CreateImageAsync(Image image);
        Task<Image> GetImageByIdAsync(Guid imageId);
        Task UpdateImageStatusAsync(Guid imageId, string status);
    }
}
