using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JAMKCourseReviewAPI.Models
{
    public class TeacherCourse
    {
        [Key] // Data doesn't have a key column, so we'll generate it automatically
        public int TeacherCourseId { get; set; }

        [Required]
        [ForeignKey("Teacher")]
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        [Required]
        [ForeignKey("Course")]
        public string CourseCode { get; set; }
        public Course Course { get; set; }
    }
}