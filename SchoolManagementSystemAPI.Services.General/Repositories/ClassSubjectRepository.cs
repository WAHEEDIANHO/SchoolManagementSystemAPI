using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories
{
    public class ClassSubjectRepository: GenericRepository<GradeSubject, AppDbContext>, IClassSubjectRepository
    {
        private readonly AppDbContext _context;
        public ClassSubjectRepository(AppDbContext context) : base(context) { _context = context; }

        public IEnumerable<GradeSubject> GetClassSubject(int GradeNumber)
        {
            //_context.Database.BeginTransaction
             return _context.Set<GradeSubject>()
                 .Include(x => x.Topics)!.ThenInclude(t => t.Lessons)                           //.Include(x => x.Grade)
                 .Where(x => x.Grade.GradeNumber == GradeNumber);
        }

        public async Task<GradeSubject> GetSingleClassSubject(int GradeNumber, string SubjectTitle)
        {
           return  await _context.Set<GradeSubject>()
               .Include(x => x.Topics)
               .FirstAsync(u => u.SubjectTitle == SubjectTitle && u.GradeNumber == GradeNumber );
        }
    }
}
