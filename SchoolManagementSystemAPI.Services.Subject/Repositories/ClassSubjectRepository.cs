using GenericRepository;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories
{
    public class ClassSubjectRepository: GenericRepository<ClassSubject, AppDbContext>, IClassSubjectRepository
    {
        private readonly AppDbContext _context;
        public ClassSubjectRepository(AppDbContext context) : base(context) { _context = context; }

        public IEnumerable<ClassSubject> GetClassSubject(int GradeNumber)
        {
            //_context.Database.BeginTransaction
             return _context.Set<ClassSubject>().Where(x => x.GradeNumber == GradeNumber);
        }

        public async Task<ClassSubject> GetSingleClassSubject(int GradeNumber, string SubjectTitle)
        {
           return await _context.Set<ClassSubject>().FindAsync(GradeNumber, SubjectTitle);
        }
    }
}
