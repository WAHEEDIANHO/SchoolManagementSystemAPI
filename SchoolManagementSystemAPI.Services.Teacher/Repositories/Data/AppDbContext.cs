using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystemAPI.Services.Teacher.Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<TeacherSchema> Teacher { get; set; }
    }
}
