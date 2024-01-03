using System.ComponentModel.DataAnnotations;

namespace JAMKCourseReviewAPI.Models
{
    public class CourseReviewInput
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string CourseCode { get; set; }

        [Required]
        [Range(1, 5)]
        public int OverallRating { get; set; }

        [Required]
        [Range(1, 5)]
        public int DifficultyRating { get; set; }

        [Required]
        [Range(1, 30)]
        public int HoursPerWeek { get; set; }

        [Required]
        [Range(1, 5)]
        public int ContentRating { get; set; }

        [Required]
        [Range(1, 5)]
        public int LectureRating { get; set; }

        [Required]
        public bool WouldTakeAgain { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 50)]
        public string ReviewText { get; set; }
    }
}