using SchoolManagementSystemAPI.Services.Student.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Student.Utils.GrpcService
{
    public interface IGrpcApplicationUserClientService
    {
        IEnumerable<UserResponseDTO> GetStudents();
        UserResponseDTO GetStudentById(string Id);
    }
}
