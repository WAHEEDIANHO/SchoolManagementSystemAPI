using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemAPI.Services.General.Migrations
{
    /// <inheritdoc />
    public partial class generalupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Webinars",
                table: "Webinars");

            migrationBuilder.AlterColumn<string>(
                name: "WebinarMinute",
                table: "Webinars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Webinars",
                table: "Webinars",
                columns: new[] { "TopicId", "WebinarDate", "WebinarHour" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Webinars",
                table: "Webinars");

            migrationBuilder.AlterColumn<string>(
                name: "WebinarMinute",
                table: "Webinars",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Webinars",
                table: "Webinars",
                columns: new[] { "TopicId", "WebinarHour", "WebinarMinute" });
        }
    }
}
