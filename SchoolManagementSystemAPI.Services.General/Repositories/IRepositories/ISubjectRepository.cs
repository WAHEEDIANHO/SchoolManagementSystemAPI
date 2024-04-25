using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories
{
    public interface ISubjectRepository: IGenericRepository<Subject, AppDbContext>
    {
        //Task<Subject> GetById(string SessionName);

    }
}
