using Microsoft.AspNetCore.Mvc;
using JAMKCourseReviewAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JAMKCourseReviewAPI.Controllers
{
    [ApiController]
    [Route("api/reviews")]
    public class CourseReviewController : ControllerBase
    {
        private readonly CourseReviewService _courseReviewService;

        public CourseReviewController(CourseReviewService service)
        {
            _courseReviewService = service;
        }

        // GET: /api/reviews?courseCode={courseCode}
        [Authorize]
        [HttpGet("reviews-for-course")]
        public async Task<ActionResult<List<CourseReview>>> GetReviewsByCourseCode([FromQuery] string courseCode)
        {
            var reviews = await _courseReviewService.GetReviewsByCourseCode(courseCode);

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // GET: /api/reviews/reviews-for-user
        [Authorize]
        [HttpGet("reviews-for-user")]
        public async Task<ActionResult<List<CourseReview>>> GetReviewsByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var reviews = await _courseReviewService.GetReviewsByUser(int.Parse(userId));

            if (reviews == null)
            {
                return NotFound();
            }

            return reviews;
        }

        // GET: /api/reviews/single-review?reviewId={reviewId}
        [Authorize]
        [HttpGet("single-review")]
        public async Task<ActionResult<CourseReview?>> GetReview([FromQuery] int reviewId)
        {
            var review = await _courseReviewService.GetReviewById(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            return review;
        }

        // POST: /api/reviews/add
        [Authorize]
        [HttpPost("add")]
        public async Task<ActionResult<CourseReview>> AddReview([FromBody] CourseReviewInput review)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newReview = new CourseReview
            {
                UserId = int.Parse(userId),
                CourseCode = review.CourseCode,
                OverallRating = review.OverallRating,
                DifficultyRating = review.DifficultyRating,
                WorkloadRating = review.WorkloadRating,
                ContentRating = review.ContentRating,
                LectureRating = review.LectureRating,
                WouldTakeAgain = review.WouldTakeAgain,
                ReviewText = review.ReviewText
            };

            var addedReview = await _courseReviewService.AddReview(newReview);
            return CreatedAtAction(nameof(GetReview), new { addedReview });
        }

        // PUT: /api/reviews/update
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateReview([FromBody] CourseReview review)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingReview = await _courseReviewService.GetReviewById(review.ReviewId);
            if (existingReview == null)
            {
                return NotFound();
            }

            if (existingReview.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            var updatedReview = await _courseReviewService.UpdateReview(review);

            if (updatedReview == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: /api/reviews/delete?reviewId={reviewId}
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReview([FromQuery] int reviewId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var existingReview = await _courseReviewService.GetReviewById(reviewId);
            if (existingReview == null)
            {
                return NotFound();
            }

            if (existingReview.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            var result = await _courseReviewService.DeleteReview(reviewId);

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}