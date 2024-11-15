using ReviewService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Repository
{
    public interface IReviewRepository
    {
        Task<Review> CreateReviewAsync(Review review);
        Task<Review> GetReviewByIdAsync(Guid id);
        Task<IEnumerable<Review>> GetReviewsByProductIdAsync(string productId);
        Task UpdateReviewAsync(Guid id, Review review);
        Task DeleteReviewAsync(Guid id);
    }
}
