using Microsoft.EntityFrameworkCore;
using JAMKCourseReviewAPI.Models;
using JAMKCourseReviewAPI.Data;
public class CourseReviewService
{
    private readonly ApplicationDbContext _context;

    public CourseReviewService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<CourseReview> AddReview(CourseReview review)
    {
        _context.CourseReviews.Add(review);
        await _context.SaveChangesAsync();
        
        _context.Entry(review).Reload();
        return review;
    }

    public async Task<CourseReview> UpdateReview(CourseReview review)
    {
        _context.Entry(review).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        _context.Entry(review).Reload();
        return review;
    }

    public async Task<bool> DeleteReview(int id)
    {
        var review = await _context.CourseReviews.FindAsync(id);
        if (review == null)
        {
            return false;
        }

        _context.CourseReviews.Remove(review);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<CourseReview>?> GetReviewsByCourseCode(string courseCode)
    {
        var courseExists = await _context.Courses.AnyAsync(c => c.CourseCode == courseCode);
        if (!courseExists)
        {
            return null;
        }

        var reviews = await _context.CourseReviews
            .Where(r => r.CourseCode == courseCode)
            .ToListAsync();

        return reviews;
    }

    public async Task<CourseReview?> GetReviewById(int id)
    {
        return await _context.CourseReviews.FindAsync(id);
    }

    public async Task<List<CourseReview>> GetAllReviews()
    {
        return await _context.CourseReviews.ToListAsync();
    }

    public async Task<List<CourseReview>> GetReviewsByUser(int userId)
    {
        return await _context.CourseReviews
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }
}