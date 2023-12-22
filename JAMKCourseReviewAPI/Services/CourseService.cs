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

    public async Task<List<TeacherCourse>> GetTeacherCourses()
    {
        return await _context.TeacherCourses
            .Include(tc => tc.Course)
            .Include(tc => tc.Teacher)
            .ToListAsync();
    }
}