using ReviewService.Models;

namespace ReviewService.Interfaces
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(Guid reviewId);
        Task UpdateReviewStatusAsync(Guid reviewId, string status);
    }
}
