using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories
{
    public interface IGradeRepository: IGenericRepository<Grade, AppDbContext>
    {
        Task<Grade> GetById(int id);
    }
}
