using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService.IGrpcService
{
    public interface IGrpcApplicationUserClientService
    {
        IEnumerable<UserResponseDTO> GetTeachers();
        UserResponseDTO GetTeacherById(string id);
    }
}
