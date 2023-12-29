using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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