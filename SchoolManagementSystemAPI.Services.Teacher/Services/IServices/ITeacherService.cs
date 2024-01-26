using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Teacher.Services.IServices
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDTO>> GetAllTeacher();
        Task<TeacherDTO> GetStudentById(string id);
        Task<bool> DeleteStudentById(string id);
        Task<bool> RegStudent(MsgRegTeacherDTO req);
    }
}
