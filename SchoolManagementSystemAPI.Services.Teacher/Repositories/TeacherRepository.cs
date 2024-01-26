using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.Data;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.IRepositories;

namespace SchoolManagementSystemAPI.Services.Teacher.Repositories
{
    public class TeacherRepository : GenericRepository<TeacherSchema, AppDbContext>, ITeacherRepository
    {
        private readonly AppDbContext _context;
        public TeacherRepository(AppDbContext context) : base(context) {
            _context= context;
        }

        public async Task<TeacherSchema> GetTeacherById (string id)
        {
            return await _context.Teacher.FirstAsync(x => x.RegId.ToLower() == id.ToLower());
        }

    }
}
