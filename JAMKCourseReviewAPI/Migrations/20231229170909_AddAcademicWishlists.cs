using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JAMKCourseReviewAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAcademicWishlists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AcademicWishlists",
                columns: table => new
                {
                    AcademicWishlistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicWishlists", x => x.AcademicWishlistId);
                    table.ForeignKey(
                        name: "FK_AcademicWishlists_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AcademicWishlists_Courses_CourseCode",
                        column: x => x.CourseCode,
                        principalTable: "Courses",
                        principalColumn: "CourseCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicWishlists_CourseCode",
                table: "AcademicWishlists",
                column: "CourseCode");

            migrationBuilder.CreateIndex(
                name: "IX_AcademicWishlists_UserId",
                table: "AcademicWishlists",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicWishlists");
        }
    }
}
