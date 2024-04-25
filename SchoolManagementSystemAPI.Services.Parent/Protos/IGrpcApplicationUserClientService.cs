using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.Parent.Protos
{
    public interface IGrpcApplicationUserClientService
    {
        IEnumerable<UserResponseDTO> GetAllParents();

        UserResponseDTO GetUserById(string id);
    }
}
