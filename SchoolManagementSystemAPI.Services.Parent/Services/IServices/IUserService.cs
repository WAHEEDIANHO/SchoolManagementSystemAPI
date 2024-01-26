using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Parent.Services.IServices
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetParentById(string id);
        Task<IEnumerable<UserResponseDTO>> GetParent();
    }
}
