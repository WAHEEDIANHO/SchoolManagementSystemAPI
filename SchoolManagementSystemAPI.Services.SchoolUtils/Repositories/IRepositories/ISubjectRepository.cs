using GenericRepository;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories
{
    public interface ISubjectRepository: IGenericRepository<Subject, AppDbContext>
    {
        //Task<Subject> GetById(string SessionName);

    }
}
