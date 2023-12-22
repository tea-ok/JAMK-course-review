using JAMKCourseReviewAPI.Models;
using JAMKCourseReviewAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            Teachers = teachers,
            Course = course,
        };
    }

    public async Task<IEnumerable<dynamic>> GetTeacherCourses()
    {
        return await _context.TeacherCourses
            .Select(tc => new 
            {
                Teacher = tc.Teacher,
                Course = tc.Course
            })
            .ToListAsync();
    }
}