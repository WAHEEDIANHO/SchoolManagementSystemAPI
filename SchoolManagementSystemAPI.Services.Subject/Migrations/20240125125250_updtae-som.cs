using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Migrations
{
    /// <inheritdoc />
    public partial class updtaesom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ClassSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectTeacher",
                table: "ClassSubjects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "SubjectTitle",
                table: "ClassSubjects",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects",
                columns: new[] { "GradeNumber", "SubjectTitle" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects");

            migrationBuilder.DropColumn(
                name: "SubjectTitle",
                table: "ClassSubjects");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectTeacher",
                table: "ClassSubjects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubjectId",
                table: "ClassSubjects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects",
                columns: new[] { "GradeNumber", "SubjectId" });
        }
    }
}
