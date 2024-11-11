using Microsoft.AspNetCore.Mvc;
using ReviewService.Interfaces;
using ReviewService.Models;

namespace ReviewService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var reviews = new List<Review>()
            {
                new Review { ReviewId = Guid.NewGuid(), Title = "Review 1", Rating = 10 },
            };
            return Ok(reviews);
        }
    }
}
