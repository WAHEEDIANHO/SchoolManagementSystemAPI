using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Teacher.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseDTO> getUserById(string username);
        Task<IEnumerable<UserResponseDTO>> getUserByRole();


    }
}
