using System;

namespace ImageService.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public byte[] Data { get; set; }
    }
}
