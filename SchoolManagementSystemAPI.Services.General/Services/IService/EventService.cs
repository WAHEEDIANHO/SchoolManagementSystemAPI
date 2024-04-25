using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public class EventService: IEventService
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<bool> CreateEvent(EventReqDTO eventReq)
    {
        await _repository.Add(_mapper.Map<Event>(eventReq));
        return true;
    }

    public async Task<IEnumerable<EventDTO>> GetEvent()
    {
        return _mapper.Map<IEnumerable<EventDTO>>(await _repository.GetAll());
    }

    public async Task<EventDTO> GetEventById(string id)
    {
        return _mapper.Map<EventDTO>(await _repository.GetByKey(id));
    }

    public async Task<bool> UpdateEvent(EventDTO eventReq)
    {
        var isEvent = await _repository.GetByKey(eventReq.EventId);
        if (isEvent != null)
        {
            _repository.Update(_mapper.Map<Event>(eventReq));
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteEvent(string id)
    {
        var isEvent = await _repository.GetByKey(id);
        if (isEvent != null)
        {
            _repository.Delete(isEvent);
            return true;
        }

        return false;
    }
}