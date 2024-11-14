using ImageService.Models;
using System;
using System.Threading.Tasks;

namespace ImageService.Interfaces
{
    public interface IImageService
    {
        Task<Image> GetImageByIdAsync(Guid id);
    }
}
