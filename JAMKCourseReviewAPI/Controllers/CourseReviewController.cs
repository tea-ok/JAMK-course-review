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

        // GET: /api/reviews/reviews-for-course?courseCode={courseCode}
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
            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }

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
            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }

            // Check if user has already reviewed this course
            var existingReviews = await _courseReviewService.GetReviewsByUser(int.Parse(userId));
            if (existingReviews != null)
            {
                foreach (var existingReview in existingReviews)
                {
                    if (existingReview.CourseCode == review.CourseCode)
                    {
                        return BadRequest("You have already reviewed this course.");
                    }
                }
            }

            var newReview = new CourseReview
            {
                UserId = int.Parse(userId),
                CourseCode = review.CourseCode,
                OverallRating = review.OverallRating,
                DifficultyRating = review.DifficultyRating,
                ContentRating = review.ContentRating,
                LectureRating = review.LectureRating,
                HoursPerWeek = review.HoursPerWeek,
                WouldTakeAgain = review.WouldTakeAgain,
                ReviewText = review.ReviewText
            };

            var addedReview = await _courseReviewService.AddReview(newReview);
            return CreatedAtAction(nameof(GetReview), new { addedReview });
        }

        // PUT: /api/reviews/update
        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateReview([FromQuery] int reviewId, [FromBody] CourseReviewInput review)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }

            var existingReview = await _courseReviewService.GetReviewById(reviewId);
            if (existingReview == null)
            {
                return NotFound();
            }

            if (existingReview.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            existingReview.CourseCode = review.CourseCode;
            existingReview.OverallRating = review.OverallRating;
            existingReview.DifficultyRating = review.DifficultyRating;
            existingReview.ContentRating = review.ContentRating;
            existingReview.LectureRating = review.LectureRating;
            existingReview.HoursPerWeek = review.HoursPerWeek;
            existingReview.WouldTakeAgain = review.WouldTakeAgain;
            existingReview.ReviewText = review.ReviewText;

            var updatedReview = await _courseReviewService.UpdateReview(existingReview);

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
            if (userId == null)
            {
                return BadRequest("User ID not found.");
            }

            var existingReview = await _courseReviewService.GetReviewById(reviewId);
            if (existingReview == null)
            {
                return NotFound();
            }

            if (existingReview.UserId != int.Parse(userId))
            {
                return Forbid();
            }

            await _courseReviewService.DeleteReview(reviewId);
            return NoContent();
        }
    }
}