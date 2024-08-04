using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class TopicRepository: GenericRepository<Topic, AppDbContext>, ITopicRepository
{
    private readonly AppDbContext _context;
    public TopicRepository(AppDbContext context): base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Topic>> GetGradeSubjectTopics(int GradeNumber, string SubjectTitle, int? TermTermNumber = null)
    {
        return TermTermNumber != null ? 
            await  _context.Set<Topic>().Include(x => x.Lessons)
            .Where(x => x.GradeSubjectGradeNumber == GradeNumber && x.GradeSubjectSubjectTitle == SubjectTitle && x.TermTermNumber == TermTermNumber)
            .ToListAsync() :
            await  _context.Set<Topic>().Include(x => x.Lessons)
                .Where(x => x.GradeSubjectGradeNumber == GradeNumber && x.GradeSubjectSubjectTitle == SubjectTitle)
                .ToListAsync();
    }

    public async Task<Topic> GetByKey(string id)
    {
        return await _context.Set<Topic>()
            .Include(x => x.Lessons)
            .AsNoTracking().FirstAsync(x => x.TopicId.ToLower() == id.ToLower());
    }
    
}