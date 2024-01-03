using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

namespace JAMKCourseReviewAPI.Models
{
    public class User : IdentityUser<int>
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public ICollection<CourseReview> Reviews { get; set; } // navigation property, reviews by this user
    }
}