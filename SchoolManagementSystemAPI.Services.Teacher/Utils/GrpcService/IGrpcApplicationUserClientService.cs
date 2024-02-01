using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.TeacherAPI;

namespace SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService
{
    public interface IGrpcApplicationUserClientService
    {
        IEnumerable<UserResponseDTO> GetTeachers();
        UserResponseDTO GetTeacherById(string id);
    }
}
