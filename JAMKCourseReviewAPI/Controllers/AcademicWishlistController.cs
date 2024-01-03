using Microsoft.AspNetCore.Mvc;
using JAMKCourseReviewAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace JAMKCourseReviewAPI.Controllers
{
    [ApiController]
    [Route("api/wishlist")]
    public class AcademicWishlistController : ControllerBase
    {
        private readonly AcademicWishlistService _wishlistService;

        public AcademicWishlistController(AcademicWishlistService wishlistService)
        {
            _wishlistService = wishlistService;
        }

        // GET: /api/wishlist
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetWishlistByUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var wishlist = await _wishlistService.GetWishlistByUserId(int.Parse(userId));
            return Ok(wishlist);
        }

        // POST: /api/wishlist
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToWishlist([FromBody] AcademicWishlistInput wishlistItem)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWishlist = await _wishlistService.GetWishlistByUserId(int.Parse(userId));

            var existingItem = userWishlist.FirstOrDefault(item => item.Course.CourseCode == wishlistItem.CourseCode);

            if (existingItem != null)
            {
                return BadRequest("Course already in Academic Wishlist");
            }

            var newWishlistItem = new AcademicWishlist 
            {
                UserId = int.Parse(userId),
                CourseCode = wishlistItem.CourseCode
            };

            await _wishlistService.AddToWishlist(newWishlistItem);
            return Ok();
        }

        // DELETE: /api/wishlist?wishlistId={wishlistId}
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveFromWishlist([FromQuery] int wishlistId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userWishlist = await _wishlistService.GetWishlistByUserId(int.Parse(userId));

            var wishlistItem = userWishlist.FirstOrDefault(item => item.AcademicWishlistId == wishlistId);

            if (wishlistItem == null)
            {
                return NotFound();
            }

            await _wishlistService.RemoveFromWishlist(wishlistId);
            return NoContent();
        }
    }
}