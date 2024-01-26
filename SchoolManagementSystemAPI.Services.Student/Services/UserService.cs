using Newtonsoft.Json;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Student.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UserResponseDTO> getUserById(string username)
        {
            HttpClient client = _httpClientFactory.CreateClient("UserAPI");
            var response = await client.GetAsync($"/api/user/{username}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if (resp.IsSuccessful) return JsonConvert.DeserializeObject<UserResponseDTO>(Convert.ToString(resp.Result));
            else return new UserResponseDTO();
        }

        public async Task<IEnumerable<UserResponseDTO>> getUserByRole()
        {
            HttpClient client = _httpClientFactory.CreateClient("UserAPI");
            var response = await client.GetAsync($"api/user/by-role?role=student");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

            if (resp.IsSuccessful) return JsonConvert.DeserializeObject<IEnumerable<UserResponseDTO>>(Convert.ToString(resp.Result));
            else return Enumerable.Empty<UserResponseDTO>();
        }
    }
}
