using GenericRepository;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories
{
    public interface IGradeRepository: IGenericRepository<Grade, AppDbContext>
    {
        Task<Grade> GetById(int id);
    }
}
