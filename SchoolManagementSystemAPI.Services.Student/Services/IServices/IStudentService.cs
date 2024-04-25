using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Repositories.IRepositories;

namespace SchoolManagementSystemAPI.Services.Student.Services.IServices
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDTO>> GetAllStudent();
        IEnumerable<StudentDTO> GetByGrade(int gradeId);
        Task<StudentDTO> GetStudentById(string id);
        Task<bool> DeleteStudentById(string id);
        Task<bool> RegStudent(MsgRegStudentDTO req);


    }
}
