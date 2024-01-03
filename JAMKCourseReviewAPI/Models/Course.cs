using System.ComponentModel.DataAnnotations;

namespace JAMKCourseReviewAPI.Models
{
    public class Course
    {
        [Key]
        public string CourseCode { get; set; }

        [Required]
        public string CourseTitle { get; set; }

        [Required]
        public float Credits { get; set; }
        public string? CourseType { get; set; }
        public string? Objective { get; set; }
        public string? TeachingMethods { get; set; }
        public string? Content { get; set; }
        public string? LearningMaterial { get; set; }
        public string? LocationType { get; set; } // teaching_method in data
        public string? Qualifications { get; set; }
        public string? EmployerConnections { get; set; }
        public string? ExamSchedule { get; set; }
        public string? InternationalConnections { get; set; }
        public string? Workload { get; set; }
        public string? ContentScheduling { get; set; }
        public string? CourseInformation { get; set; }
        public string? FurtherInformation { get; set; }
        public string? EvaluationScale { get; set; }
        public string? FacultyName { get; set; } // unit_title in data
        public float? OnlineCredits { get; set; }
        public float? ContactCredits { get; set; }
        public float? MinSeats { get; set; }
        public float? MaxSeats { get; set; }
        public ICollection<Review> Reviews { get; set; } // navigation property, reviews for this course
    }
}