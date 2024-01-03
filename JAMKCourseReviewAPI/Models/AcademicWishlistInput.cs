using System.ComponentModel.DataAnnotations;

namespace JAMKCourseReviewAPI.Models
{
    public class AcademicWishlistInput
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string CourseCode { get; set; }
    }
}