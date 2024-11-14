using Microsoft.AspNetCore.Mvc;
using ImageService.Models;
using Azure.Storage.Blobs;

namespace ImageService.Services
{
    public class ImageService : IImageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ImageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public Task<Image> GetImageByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<Image> IImageService.UploadImageAsync(string blobId, IFormFile image)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient("images");
            var blobClient = containerClient.GetBlobClient(blobId);

            using (var stream = image.OpenReadStream())
            {
                blobClient.Upload(stream, true);
            }
            return Task.FromResult(new Image { Id = Guid.NewGuid(), FileName = blobId });
        }
    }
}
