using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Repositories.IRepositories
{
    public interface IAuthRepository
    {
        public Task<bool> RegisterUser(RegisterRequestDTO registerRequestDTO);
        public Task<bool> AssignRole(RegisterRequestDTO registerRequestDTO);
        public Task<ApplicationUser> GetUserById(string id);
        Task<ApplicationUser> GetUserByUsername(string username);

        public IEnumerable<ApplicationUser> GetUsersByRole(string role);
        public IEnumerable<ApplicationUser> GetAllUsers();

        public Task<bool> CheckPassword(ApplicationUser user,string password);
        public Task<IList<string>> GetRoles(ApplicationUser user);
        public string CreateRole(string role);
        void Remove(ApplicationUser user);
    }
}
