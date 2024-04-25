using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemAPI.Services.General.Migrations
{
    /// <inheritdoc />
    public partial class generalinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttendanceHeader",
                columns: table => new
                {
                    AttendanceHeaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeNumber = table.Column<int>(type: "int", nullable: false),
                    SessionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    TotalExpectedAttendance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceHeader", x => x.AttendanceHeaderId);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeTeacher = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeNumber);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    SessionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SessionStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.SessionName);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectTitle);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceDetail",
                columns: table => new
                {
                    AttendanceDate = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttendanceHeaderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AttendanceTimeIn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttendanceTimeOut = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceDetail", x => new { x.AttendanceDate, x.UserId });
                    table.ForeignKey(
                        name: "FK_AttendanceDetail_AttendanceHeader_AttendanceHeaderId",
                        column: x => x.AttendanceHeaderId,
                        principalTable: "AttendanceHeader",
                        principalColumn: "AttendanceHeaderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventMinute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SessionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Sessions_SessionName",
                        column: x => x.SessionName,
                        principalTable: "Sessions",
                        principalColumn: "SessionName",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeSubjects",
                columns: table => new
                {
                    GradeNumber = table.Column<int>(type: "int", nullable: false),
                    SubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SubjectTeacher = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeSubjects", x => new { x.GradeNumber, x.SubjectTitle });
                    table.ForeignKey(
                        name: "FK_GradeSubjects_Grades_GradeNumber",
                        column: x => x.GradeNumber,
                        principalTable: "Grades",
                        principalColumn: "GradeNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeSubjects_Subjects_SubjectTitle",
                        column: x => x.SubjectTitle,
                        principalTable: "Subjects",
                        principalColumn: "SubjectTitle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Term = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectGradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectSubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.TopicId);
                    table.ForeignKey(
                        name: "FK_Topics_GradeSubjects_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                        columns: x => new { x.GradeSubjectGradeNumber, x.GradeSubjectSubjectTitle },
                        principalTable: "GradeSubjects",
                        principalColumns: new[] { "GradeNumber", "SubjectTitle" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Objectives = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Transcript = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TopicId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.LessonId);
                    table.ForeignKey(
                        name: "FK_Lessons_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Webinars",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WebinarHour = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WebinarMinute = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WebinarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherInCharge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebinarDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webinars", x => new { x.TopicId, x.WebinarHour, x.WebinarMinute });
                    table.ForeignKey(
                        name: "FK_Webinars_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetail_AttendanceHeaderId",
                table: "AttendanceDetail",
                column: "AttendanceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SessionName",
                table: "Events",
                column: "SessionName");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSubjects_SubjectTitle",
                table: "GradeSubjects",
                column: "SubjectTitle");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TopicId",
                table: "Lessons",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "Topics",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttendanceDetail");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Webinars");

            migrationBuilder.DropTable(
                name: "AttendanceHeader");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "GradeSubjects");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
