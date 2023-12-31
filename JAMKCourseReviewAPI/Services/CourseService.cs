using JAMKCourseReviewAPI.Data;
using Microsoft.EntityFrameworkCore;

public class CourseService
{
    private readonly ApplicationDbContext _context;

    public CourseService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<dynamic> GetCourseByCode(string courseCode)
    {
        var course = await _context.Courses
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

        return new 
        {
            Course = course,
            Teachers = teachers
        };
    }

    public async Task<IEnumerable<dynamic>> GetTeacherCourses()
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