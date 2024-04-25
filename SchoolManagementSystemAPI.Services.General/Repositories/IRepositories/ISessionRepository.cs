using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories
{
    public interface ISessionRepository: IGenericRepository<Session, AppDbContext>
    {
        public Task<Session> GetById(string SessionName);

    }
}
    