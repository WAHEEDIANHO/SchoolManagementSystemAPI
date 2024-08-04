using System.Collections;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface IWebinarService
{
    Task AddWebinar(WebinarReqDto webinar);
    Task RemoveWebinar(string webinarId, Dictionary<string, string>? filter = null);
    Task<IEnumerable<Webinar>> GetUpcomingWebinars(int GradeNumber);
    Task<IEnumerable<Webinar>> GetAllWebinars(Dictionary<string, string>? filter = null);
    Task<Webinar> GetCurrentWebinar(int GradeNumber);

}