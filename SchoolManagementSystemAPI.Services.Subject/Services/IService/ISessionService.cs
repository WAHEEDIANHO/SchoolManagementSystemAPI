using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService
{
    public interface ISessionService
    {
        Task<IEnumerable<SessionDTO>> getAllClass();
        Task<SessionDTO> getSessionById(string id);
        Task<SessionDTO> deleteSessionbyId(string id);
        Task<bool> AddClass(SessionDTO schSession);
    }
}
