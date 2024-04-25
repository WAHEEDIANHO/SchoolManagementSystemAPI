using GenericRepository;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories
{
    public interface ISessionRepository: IGenericRepository<Session, AppDbContext>
    {
        public Task<Session> GetById(string SessionName);

    }
}
    