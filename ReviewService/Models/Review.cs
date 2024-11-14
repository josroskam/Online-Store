using System;

namespace ReviewService.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
    }
}
