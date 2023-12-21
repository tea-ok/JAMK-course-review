using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMKCourseReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOriginalTeacherId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OriginalTeacherId",
                table: "Teachers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OriginalTeacherId",
                table: "Teachers");
        }
    }
}
