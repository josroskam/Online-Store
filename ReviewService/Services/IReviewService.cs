using ReviewService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviewsForProductAsync(Guid productId);
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(Guid reviewId);
        Task UpdateReviewStatusAsync(Guid reviewId, Review review);
    }
}
