//using GenericRepository;
using GenericRepository;
using SchoolManagementSystemAPI.Services.Teacher.Repositories.Data;

namespace SchoolManagementSystemAPI.Services.Teacher.Repositories.IRepositories
{
    public interface ITeacherRepository: IGenericRepository<TeacherSchema, AppDbContext>
    {
        Task<TeacherSchema> GetTeacherById(string id);
    }
}
