using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Teacher.Services.IServices
{
    public interface ITeacherService
    {
        Task<IEnumerable<TeacherDTO>> GetAllTeacher();
        Task<TeacherDTO> GetTeacherById(string id);
        Task<bool> DeleteTeacherById(string id);
        Task<bool> RegTeacher(MsgRegTeacherDTO req);
    }
}
