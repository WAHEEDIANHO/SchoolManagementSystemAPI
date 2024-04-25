using GenericRepository;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories;

public class LessonRepository: GenericRepository<Lesson, AppDbContext>, ILessonRepository
{
    private readonly AppDbContext _context;
    public LessonRepository(AppDbContext context): base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lesson>> GetTopicLessons(string topicId)
    {
       return await _context.Set<Lesson>().Where(x => x.TopicId.ToLower() == topicId.ToLower()).ToListAsync();
    }
    
}