using ReviewService.Models;
using ReviewService.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<Review> CreateReviewAsync(Review review)
        {
            if (string.IsNullOrEmpty(review.ProductId))
            {
                throw new ArgumentException("ProductId cannot be null or empty.");
            }

            if (review.Rating < 1 || review.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5.");
            }

            return await _reviewRepository.CreateReviewAsync(review);
        }

        public async Task<Review> GetReviewByIdAsync(Guid id)
        {
            return await _reviewRepository.GetReviewByIdAsync(id);
        }

        public async Task<IEnumerable<Review>> GetReviewsByProductIdAsync(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                throw new ArgumentException("ProductId cannot be null or empty.");
            }

            return await _reviewRepository.GetReviewsByProductIdAsync(productId);
        }

        public async Task UpdateReviewAsync(Guid id, Review review)
        {
            if (string.IsNullOrEmpty(review.ProductId))
            {
                throw new ArgumentException("ProductId cannot be null or empty.");
            }

            await _reviewRepository.UpdateReviewAsync(id, review);
        }

        public async Task DeleteReviewAsync(Guid id)
        {
            await _reviewRepository.DeleteReviewAsync(id);
        }
    }
}
