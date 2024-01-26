using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}
