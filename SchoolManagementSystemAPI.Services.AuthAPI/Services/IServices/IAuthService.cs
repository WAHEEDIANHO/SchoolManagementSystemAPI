using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices
{
    public interface IAuthService<T> where T : RegisterRequestDTO
    {
        public Task<bool> Register(T requestDTO);
        public Task<LoginResponseDTO> Login(LoginRequestDTO requestDTO);
        public string CreateRole(string role);

        public Task<UserResponseDTO> GetUser(string id);
        public IEnumerable<UserResponseDTO> GetUsersByRole(string role);
        public IEnumerable<UserResponseDTO> GetUsersByGender(string gender);

        public IEnumerable<UserResponseDTO> GetAllUsers(Dictionary<string, string>? filter = null);

    }
}
