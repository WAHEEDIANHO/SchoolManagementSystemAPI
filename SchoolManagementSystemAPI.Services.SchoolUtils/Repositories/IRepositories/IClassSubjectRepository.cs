using GenericRepository;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories
{
    public interface IClassSubjectRepository: IGenericRepository<ClassSubject, AppDbContext>
    {
        IEnumerable<ClassSubject> GetClassSubject(int GradeNumber);
        Task<ClassSubject> GetSingleClassSubject(int GradeNumber, string SubjectTitle);
    }
}
