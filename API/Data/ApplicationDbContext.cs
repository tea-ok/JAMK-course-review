using Microsoft.EntityFrameworkCore;
using UniversityCourseReviewAPI.Models;

namespace UniversityCourseReviewAPI.Data
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