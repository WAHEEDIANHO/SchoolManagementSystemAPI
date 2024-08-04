using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;

public interface ILessonQuestionRepository: IGenericRepository<LessonQuestion, AppDbContext>
{
    
}