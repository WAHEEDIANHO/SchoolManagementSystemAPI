using GenericRepository;
using SchoolManagementSystemAPI.Services.Student.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.Student.Repositories.IRepositories
{
    public interface IStudentRepository: IGenericRepository<StudentSchema, AppDbContext>
    {
        Task<StudentSchema> GetById(string id);
        IEnumerable<StudentSchema> GetByGrade(int GradeId);

    }
}
