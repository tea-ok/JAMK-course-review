using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JAMKCourseReviewAPI.Models
{
    [Table("CourseReviews")] // Had to add this because EF Core was trying to create a table called CourseReview
    public class CourseReview
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        [Required]
        [ForeignKey("Course")]
        public string CourseCode { get; set; }
        
        [JsonIgnore]
        public Course Course { get; set; }

        [Required]
        [Range(1, 5)]
        public int OverallRating { get; set; }

        [Required]
        [Range(1, 5)]
        public int DifficultyRating { get; set; }

        [Required]
        [Range(1, 30)]
        public int HoursPerWeek { get; set; } // Hours per week spent on the course

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