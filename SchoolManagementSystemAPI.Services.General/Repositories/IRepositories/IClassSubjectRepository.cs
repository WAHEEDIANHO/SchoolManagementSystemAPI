using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories
{
    public interface IClassSubjectRepository: IGenericRepository<GradeSubject, AppDbContext>
    {
        IEnumerable<GradeSubject> GetClassSubject(int GradeNumber);
        Task<GradeSubject> GetSingleClassSubject(int GradeNumber, string SubjectTitle);
    }
}
