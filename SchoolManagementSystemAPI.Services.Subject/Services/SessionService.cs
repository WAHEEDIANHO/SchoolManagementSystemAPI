using AutoMapper;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repo;
        private readonly IMapper _mapper;

        public SessionService(ISessionRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> AddClass(SessionDTO schSession)
        {
            try
            {
                Session reg = _mapper.Map<Session>(schSession);
                await _repo.Add(reg);
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<SessionDTO> deleteSessionbyId(string id)
        {
            try
            {
                var res = await _repo.GetById(id);
                _repo.Delete(res);
                return _mapper.Map<SessionDTO>(res);
            }
            catch (Exception e) { throw e; }
        }

        public async Task<IEnumerable<SessionDTO>> getAllClass()
        {
            try
            {
                var result = await _repo.GetAll();
                return _mapper.Map<IEnumerable<SessionDTO>>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<SessionDTO> getSessionById(string id)
        {
            try
            {
                var res = await _repo.GetById(id);
                return _mapper.Map<SessionDTO>(res);
            }
            catch (Exception e) { throw e; }
        }
    }
}
