using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.Student.Repositories.Data;
using SchoolManagementSystemAPI.Services.Student.Repositories.IRepositories;

namespace SchoolManagementSystemAPI.Services.Student.Repositories
{
    public class StudentRepository : GenericRepository<StudentSchema, AppDbContext>, IStudentRepository
    {
        private readonly AppDbContext _context;
        public StudentRepository(AppDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<StudentSchema> GetById(string id)
        {
            return await _context.Students.FirstAsync(u => u.RegId.ToLower() == id.ToLower());

        }
    }
}
