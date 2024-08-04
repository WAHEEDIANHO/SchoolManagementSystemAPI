using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagementSystemAPI.Services.General.Migrations
{
    /// <inheritdoc />
    public partial class generalinitdb : Migration
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
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    SessionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TermNumber = table.Column<int>(type: "int", nullable: false),
                    TermStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => new { x.SessionName, x.TermNumber });
                    table.ForeignKey(
                        name: "FK_Terms_Sessions_SessionName",
                        column: x => x.SessionName,
                        principalTable: "Sessions",
                        principalColumn: "SessionName",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_GradeSubjects_Subjects_SubjectTitle",
                        column: x => x.SubjectTitle,
                        principalTable: "Subjects",
                        principalColumn: "SubjectTitle",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TopicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermSessionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TermTermNumber = table.Column<int>(type: "int", nullable: false),
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
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Topics_Terms_TermSessionName_TermTermNumber",
                        columns: x => new { x.TermSessionName, x.TermTermNumber },
                        principalTable: "Terms",
                        principalColumns: new[] { "SessionName", "TermNumber" },
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Webinars",
                columns: table => new
                {
                    TopicId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WebinarDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WebinarHour = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    WebinarId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeacherInCharge = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebinarMinute = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Webinars", x => new { x.TopicId, x.WebinarDate, x.WebinarHour });
                    table.ForeignKey(
                        name: "FK_Webinars_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "TopicId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    AssessmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssessmentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    DateSchedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourSchedule = table.Column<int>(type: "int", nullable: false),
                    MinuteSchedule = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeSubjectGradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectSubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.AssessmentId);
                    table.ForeignKey(
                        name: "FK_Assessments_GradeSubjects_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                        columns: x => new { x.GradeSubjectGradeNumber, x.GradeSubjectSubjectTitle },
                        principalTable: "GradeSubjects",
                        principalColumns: new[] { "GradeNumber", "SubjectTitle" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assessments_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AssignmentContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeadlineHour = table.Column<int>(type: "int", nullable: false),
                    DeadlineMinute = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeSubjectGradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectSubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_GradeSubjects_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                        columns: x => new { x.GradeSubjectGradeNumber, x.GradeSubjectSubjectTitle },
                        principalTable: "GradeSubjects",
                        principalColumns: new[] { "GradeNumber", "SubjectTitle" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Assignments_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Discussions",
                columns: table => new
                {
                    DisscusionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiscussionDisscusionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DissusionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeSubjectGradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectSubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discussions", x => x.DisscusionId);
                    table.ForeignKey(
                        name: "FK_Discussions_Discussions_DiscussionDisscusionId",
                        column: x => x.DiscussionDisscusionId,
                        principalTable: "Discussions",
                        principalColumn: "DisscusionId");
                    table.ForeignKey(
                        name: "FK_Discussions_GradeSubjects_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                        columns: x => new { x.GradeSubjectGradeNumber, x.GradeSubjectSubjectTitle },
                        principalTable: "GradeSubjects",
                        principalColumns: new[] { "GradeNumber", "SubjectTitle" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Discussions_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "LessonQuestions",
                columns: table => new
                {
                    LessonQuestionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GradeSubjectGradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectSubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonQuestions", x => x.LessonQuestionId);
                    table.ForeignKey(
                        name: "FK_LessonQuestions_GradeSubjects_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                        columns: x => new { x.GradeSubjectGradeNumber, x.GradeSubjectSubjectTitle },
                        principalTable: "GradeSubjects",
                        principalColumns: new[] { "GradeNumber", "SubjectTitle" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_LessonQuestions_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NoteContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeSubjectGradeNumber = table.Column<int>(type: "int", nullable: false),
                    GradeSubjectSubjectTitle = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_GradeSubjects_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                        columns: x => new { x.GradeSubjectGradeNumber, x.GradeSubjectSubjectTitle },
                        principalTable: "GradeSubjects",
                        principalColumns: new[] { "GradeNumber", "SubjectTitle" },
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Notes_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "Assessments",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_LessonId",
                table: "Assessments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "Assignments",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_LessonId",
                table: "Assignments",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceDetail_AttendanceHeaderId",
                table: "AttendanceDetail",
                column: "AttendanceHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_DiscussionDisscusionId",
                table: "Discussions",
                column: "DiscussionDisscusionId");

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "Discussions",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_Discussions_LessonId",
                table: "Discussions",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_SessionName",
                table: "Events",
                column: "SessionName");

            migrationBuilder.CreateIndex(
                name: "IX_GradeSubjects_SubjectTitle",
                table: "GradeSubjects",
                column: "SubjectTitle");

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuestions_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "LessonQuestions",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_LessonQuestions_LessonId",
                table: "LessonQuestions",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_TopicId",
                table: "Lessons",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "Notes",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_LessonId",
                table: "Notes",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_GradeSubjectGradeNumber_GradeSubjectSubjectTitle",
                table: "Topics",
                columns: new[] { "GradeSubjectGradeNumber", "GradeSubjectSubjectTitle" });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_TermSessionName_TermTermNumber",
                table: "Topics",
                columns: new[] { "TermSessionName", "TermTermNumber" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "AttendanceDetail");

            migrationBuilder.DropTable(
                name: "Discussions");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "LessonQuestions");

            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "Webinars");

            migrationBuilder.DropTable(
                name: "AttendanceHeader");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.DropTable(
                name: "GradeSubjects");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Sessions");
        }
    }
}
