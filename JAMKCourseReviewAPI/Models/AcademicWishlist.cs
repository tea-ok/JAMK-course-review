using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMKCourseReviewAPI.Models
{
    public class AcademicWishlist
    {
        [Key]
        public int AcademicWishlistId { get; set; }
        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
        [Required]
        [ForeignKey("Course")]
        public string CourseCode { get; set; }
        public Course Course { get; set; }
    }
}