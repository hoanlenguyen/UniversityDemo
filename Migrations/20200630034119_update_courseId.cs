using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDemo.Migrations
{
    public partial class update_courseId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseID",
                table: "Courses",
                column: "CourseID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseID",
                table: "Courses");
        }
    }
}
