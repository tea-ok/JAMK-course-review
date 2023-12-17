using Microsoft.EntityFrameworkCore;
using JAMKCourseReviewAPI.Models;

namespace JAMKCourseReviewAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        // Add other DbSets for other models later
    }
}