using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Data;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories
{
    public class SessionRepository: GenericRepository<Session, AppDbContext>, IRepositories.ISessionRepository
    {
        private readonly AppDbContext _context;

        public SessionRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Session> GetById(string SessionName)
        {
            return await _context.Set<Session>().FirstAsync(x => x.SessionName == SessionName);
        }
    }
}
