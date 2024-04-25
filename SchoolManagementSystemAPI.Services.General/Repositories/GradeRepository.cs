using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class GradeRepository : GenericRepository<Grade, AppDbContext>, IRepositories.IGradeRepository
    {
        private readonly AppDbContext _context;
        public GradeRepository(AppDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<Grade> GetById(int GradeNumber)
        {
            return await _context.Set<Grade>().FirstAsync(x => x.GradeNumber == GradeNumber);
        }
    }
}
