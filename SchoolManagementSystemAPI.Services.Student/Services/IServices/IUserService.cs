using SchoolManagementSystemAPI.Services.Student.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Student.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseDTO> getUserById(string username);
        Task<IEnumerable<UserResponseDTO>> getUserByRole();

    }
}
