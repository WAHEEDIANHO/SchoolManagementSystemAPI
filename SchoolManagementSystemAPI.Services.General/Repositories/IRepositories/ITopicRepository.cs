using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;

public interface ITopicRepository: IGenericRepository<Topic, AppDbContext>
{
    Task<IEnumerable<Topic>> GetGradeSubjectTopics(int GradeNumber, string SubjectTitle, int? TermTermNumber= null);
}