using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystemAPI.Services.Parent.Repositories.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParentSchema> Parent { get; set; }
    }
}
