using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
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

        public async Task<IEnumerable<SessionDTO>> getAllClass(Dictionary<string, string>? filter = null)
        {
            try
            {
                var result = await _repo.GetAll(filter);
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
