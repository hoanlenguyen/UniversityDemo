using Microsoft.EntityFrameworkCore.Migrations;

namespace UniversityDemo.Migrations
{
    public partial class update_StudentTable_add_userId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "StudentID",
                table: "Enrollments",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "EnrollmentID",
                table: "Enrollments",
                newName: "EnrollmentId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Enrollments",
                newName: "CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentID",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseID",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseId");

            migrationBuilder.RenameColumn(
                name: "CourseID",
                table: "Courses",
                newName: "CourseId");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Courses_CourseId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Enrollments",
                newName: "StudentID");

            migrationBuilder.RenameColumn(
                name: "EnrollmentId",
                table: "Enrollments",
                newName: "EnrollmentID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Enrollments",
                newName: "CourseID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_StudentId",
                table: "Enrollments",
                newName: "IX_Enrollments_StudentID");

            migrationBuilder.RenameIndex(
                name: "IX_Enrollments_CourseId",
                table: "Enrollments",
                newName: "IX_Enrollments_CourseID");

            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "Courses",
                newName: "CourseID");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Courses_CourseID",
                table: "Enrollments",
                column: "CourseID",
                principalTable: "Courses",
                principalColumn: "CourseID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentID",
                table: "Enrollments",
                column: "StudentID",
                principalTable: "Students",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
