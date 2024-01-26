using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystemAPI.Services.Student.Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<StudentSchema> Students { get; set; }
    }
}
