using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDemo.Migrations
{
    public partial class update_courseId_Unique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CourseID",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.DropColumn(
                name: "CourseID",
                table: "Courses");
        }
    }
}
