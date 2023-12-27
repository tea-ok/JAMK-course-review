using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JAMKCourseReviewAPI.Models;
using JAMKCourseReviewAPI.Data;

public class AcademicWishlistService
{
    private readonly ApplicationDbContext _context;

    public AcademicWishlistService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<dynamic>> GetWishlistByUserId(int userId)
    {
        var wishlistCourses = await _context.AcademicWishlists
            .Where(w => w.UserId == userId)
            .Select(w => w.Course)
            .ToListAsync();

        var result = new List<dynamic>();

        foreach (var course in wishlistCourses)
        {
            var teachers = await _context.TeacherCourses
                .Where(tc => tc.CourseCode == course.CourseCode)
                .Select(tc => tc.Teacher)
                .ToListAsync();

            result.Add(new 
            {
                Course = course,
                Teachers = teachers
            });
        }

        return result;
    }

    public async Task AddToWishlist(AcademicWishlist wishlistItem)
    {
        _context.AcademicWishlists.Add(wishlistItem);
        await _context.SaveChangesAsync();
    }

    public async Task RemoveFromWishlist(int wishlistId)
    {
        var wishlistItem = await _context.AcademicWishlists.FindAsync(wishlistId);
        if (wishlistItem != null)
        {
            _context.AcademicWishlists.Remove(wishlistItem);
            await _context.SaveChangesAsync();
        }
    }
}