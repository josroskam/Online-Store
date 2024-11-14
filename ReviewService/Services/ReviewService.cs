using ReviewService.Services;
using ReviewService.Models;

namespace ReviewService.Services
{
    public class ReviewService : IReviewService
    {
        public ReviewService() { }

        public Task<Review> CreateReviewAsync(Review review)
        {
            throw new NotImplementedException();
        }

        public Task<Review> GetReviewByIdAsync(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetReviewsForProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateReviewStatusAsync(Guid reviewId, Review review)
        {
            throw new NotImplementedException();
        }
    }
}
