using JAMKCourseReviewAPI.Data;
using Microsoft.EntityFrameworkCore;

public class CourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<dynamic?> GetCourseByCode(string courseCode)
    {
        var course = await _context.Courses
            .Include(c => c.Reviews)
            .Where(c => c.CourseCode == courseCode)
            .FirstOrDefaultAsync();

        if (course == null)
        {
            return null;
        }

        var teachers = await _context.TeacherCourses
            .Where(tc => tc.CourseCode == course.CourseCode)
            .Select(tc => tc.Teacher)
            .ToListAsync();

        var AvgRatings = course.Reviews.Count != 0 ? new 
        {
            OverallRating = course.Reviews.Average(r => r.OverallRating),
            DifficultyRating = course.Reviews.Average(r => r.DifficultyRating),
            HoursPerWeek = course.Reviews.Average(r => r.HoursPerWeek),
            ContentRating = course.Reviews.Average(r => r.ContentRating),
            LectureRating = course.Reviews.Average(r => r.LectureRating),
            WouldTakeAgainPercentage = course.Reviews.Average(r => r.WouldTakeAgain ? 1 : 0) * 100
        } : null;

        return new 
        {
            Course = course,
            Teachers = teachers,
            AvgRatings = AvgRatings
        };
    }

    public async Task<IEnumerable<dynamic>> GetTeacherCourses() // Gets courses + their teachers
    {
        return await _context.TeacherCourses
            .Include(tc => tc.Course)
            .GroupBy(tc => tc.Course)
            .Select(g => new 
            {
                Course = g.Key,
                Teachers = g.Select(tc => tc.Teacher).ToList()
            })
            .ToListAsync();
    }
}