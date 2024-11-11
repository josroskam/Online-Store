namespace ReviewService.Models
{
    public class Review
    {
        public Guid ReviewId { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
    }
}
