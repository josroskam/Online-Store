using Microsoft.AspNetCore.Mvc;
using ReviewService.Services;
using ReviewService.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

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

        // Get all reviews by ProductId
        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetReviewsByProductId(string productId)
        {
            if (string.IsNullOrEmpty(productId))
            {
                return BadRequest("ProductId cannot be null or empty.");
            }

            var reviews = await _reviewService.GetReviewsByProductIdAsync(productId);
            return Ok(reviews);
        }

        // Get a specific review by Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(Guid id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound($"Review with ID {id} not found.");
            }

            return Ok(review);
        }

        // Create a new review
        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review data is null.");
            }

            var createdReview = await _reviewService.CreateReviewAsync(review);
            return CreatedAtAction(nameof(GetReviewById), new { id = createdReview.Id }, createdReview);
        }

        // Update an existing review
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(Guid id, [FromBody] Review review)
        {
            if (review == null)
            {
                return BadRequest("Review data is null.");
            }

            try
            {
                await _reviewService.UpdateReviewAsync(id, review);
                return NoContent(); // 204 No Content
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // Delete a review
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(Guid id)
        {
            try
            {
                await _reviewService.DeleteReviewAsync(id);
                return NoContent(); // 204 No Content
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
