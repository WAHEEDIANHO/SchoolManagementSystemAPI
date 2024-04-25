using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class WebinarRepository: GenericRepository<Webinar, AppDbContext>, IWebinarRepository
{
    private readonly AppDbContext _dbContext;

    public WebinarRepository(AppDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
   
    public async Task<IEnumerable<Webinar>> GetUpcomingWebinars(int GradeNumber)
    {
        var currentDate = DateTime.Now;
        var webinars = await _dbContext.Set<Webinar>()
            .OrderBy(x => x.WebinarDate).ThenBy(x => x.WebinarHour).ThenBy(x => x.WebinarMinute)
            .Include(x => x.Topic)
            .Where(x => (x.WebinarDate.Year >= currentDate.Year && x.WebinarDate.Month >= currentDate.Month &&
                         x.WebinarDate.Day >= currentDate.Day) && x.Topic.GradeSubjectGradeNumber == GradeNumber)
            .ToListAsync();            ;
        return webinars;
    }
    
    public async Task<Webinar> GetCurrentDateWebinar(int GradeNumber)
    {
        var currentDate = DateTime.Now;
        var data = await _dbContext.Set<Webinar>().Include(x => x.Topic)
            .OrderBy(x => x.WebinarDate).ThenBy(x => x.WebinarHour).ThenBy(x => x.WebinarMinute)
            .FirstOrDefaultAsync(x => ((x.WebinarDate.Year == currentDate.Year && x.WebinarDate.Month == currentDate.Month &&
                               x.WebinarDate.Day == currentDate.Day) && x.Topic.GradeSubjectGradeNumber == GradeNumber));
        // if (data == null) return new Webinar();
        return data;
    }
}