using System.ComponentModel.DataAnnotations;

namespace JAMKCourseReviewAPI.Models
{
    public class AcademicWishlistInputModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string CourseCode { get; set; }
    }
}