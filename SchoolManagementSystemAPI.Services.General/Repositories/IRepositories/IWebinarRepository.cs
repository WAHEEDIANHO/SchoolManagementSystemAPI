using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;

public interface IWebinarRepository: IGenericRepository<Webinar, AppDbContext>
{
    Task<IEnumerable<Webinar>> GetUpcomingWebinars(int GradeNumber);
    
    Task<Webinar> GetCurrentDateWebinar(int GradeNumber);

}