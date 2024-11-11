using Microsoft.AspNetCore.Mvc;

namespace ImageService.Models
{
    public class Image 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Url { get; set; }
    }
}
