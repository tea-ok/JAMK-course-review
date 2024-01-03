using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMKCourseReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCourseReviewHoursPerWeek : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkloadRating",
                table: "CourseReviews",
                newName: "HoursPerWeek");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HoursPerWeek",
                table: "CourseReviews",
                newName: "WorkloadRating");
        }
    }
}
