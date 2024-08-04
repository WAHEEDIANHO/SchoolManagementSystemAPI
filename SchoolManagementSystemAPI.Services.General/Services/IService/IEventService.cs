using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface IEventService
{
    Task<bool> CreateEvent(EventReqDTO eventReq);
    Task<IEnumerable<EventDTO>> GetEvent(Dictionary<string, string>? filter = null);
    Task<EventDTO> GetEventById(string id);
    Task<bool> UpdateEvent(EventDTO eventReq);
    Task<bool> DeleteEvent(string id);
}