using Microsoft.AspNetCore.Mvc;
using ReviewService.Services;
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
                new Review { Id = Guid.NewGuid(), ProductId = "", Content = "Review 1", Rating = 10 },
            };
            return Ok(reviews);
        }
    }
}
