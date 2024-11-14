using ImageService.Models;
using System;
using System.Threading.Tasks;

namespace ImageService.Services
{
    public interface IImageService
    {
        Task<Image> GetImageByIdAsync(Guid id);
    }
}
