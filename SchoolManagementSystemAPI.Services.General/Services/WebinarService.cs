using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public class WebinarService: IWebinarService
{
    private readonly IMapper _mapper;
    private readonly IWebinarRepository _repository;

    public WebinarService(
        IWebinarRepository repository,
        IMapper mapper
        )
    {
        _mapper = mapper;
        _repository = repository;
    }


    public async Task<IEnumerable<Webinar>> GetAllWebinars(Dictionary<string, string>? filter = null)
    {
        var webinars = await _repository.GetAll(filter);
        return _mapper.Map<IEnumerable<Webinar>>(webinars);
    }

    public async Task AddWebinar(WebinarReqDto webinar)
    {
        Webinar newWebinar = _mapper.Map<Webinar>(webinar);
       await _repository.Add(newWebinar);
    }

    public async Task RemoveWebinar(string webinarId, Dictionary<string, string>? filter = null)
    {
        IEnumerable<Webinar> webinars = await _repository.GetAll(filter);
        if (webinars != null)
        {
            Webinar delWebinar = webinars.First(u => u.WebinarId == webinarId);
            _repository.Delete(delWebinar);
        }
    }

    public async Task<IEnumerable<Webinar>> GetUpcomingWebinars(int GradeNumber)
    {
        return await _repository.GetUpcomingWebinars(GradeNumber);
    }
    
    public async Task<Webinar> GetCurrentWebinar(int GradeNumber)
    {
       return await _repository.GetCurrentDateWebinar(GradeNumber);
    }
}