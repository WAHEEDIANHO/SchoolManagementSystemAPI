using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using Event = SchoolManagementSystemAPI.Services.General.Repositories.Schema.Event;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<GradeSubject> GradeSubjects { get; set; }
        public DbSet<AttendanceHeader> AttendanceHeader { get; set; }
        public DbSet<AttendanceDetail> AttendanceDetail { get; set; }
        public DbSet<Notification> Notification { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Webinar> Webinars { get; set; }
    }
}
