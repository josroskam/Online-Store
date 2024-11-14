using ReviewService.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewService.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetReviewsForProductAsync(Guid productId);
    }
}
