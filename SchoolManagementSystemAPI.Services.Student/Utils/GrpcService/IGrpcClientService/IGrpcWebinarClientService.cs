using SchoolManagementSystemAPI.Services.Student.Model.DTO;

namespace SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

public interface IGrpcWebinarClientService
{
    Task<IEnumerable<Webinar>> GetUpcomingWebinars(int GradeNumber);
    Task<Webinar> GetCurrentWebinar(int GradeNumber);
}