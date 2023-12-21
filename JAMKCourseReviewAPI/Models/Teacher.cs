using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMKCourseReviewAPI.Models
{
    public class Teacher
    {
        [Key]
        public int TeacherId { get; set; }
        public int OriginalTeacherId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}