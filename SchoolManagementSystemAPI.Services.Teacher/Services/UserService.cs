using Newtonsoft.Json;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Teacher.Services
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
            try
            {
                HttpClient client = _httpClientFactory.CreateClient("UserAPI");
                var response = await client.GetAsync($"api/user/{username}");
                var apiContent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

                if (resp.IsSuccessful) return JsonConvert.DeserializeObject<UserResponseDTO>(Convert.ToString(resp.Result));
                else return new();
            }
            catch (Exception ex)
            {
                return new();
            }
        }

        public async Task<IEnumerable<UserResponseDTO>> getUserByRole()
        {
           try
            {
                HttpClient client = _httpClientFactory.CreateClient("UserAPI");
                var response = await client.GetAsync($"api/user/by-role?role=teacher");
                var apiCOntent = await response.Content.ReadAsStringAsync();
                var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiCOntent);

                if (resp.IsSuccessful) return JsonConvert.DeserializeObject<IEnumerable<UserResponseDTO>>(Convert.ToString(resp.Result));
                else return Enumerable.Empty<UserResponseDTO>();
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<UserResponseDTO>();
            }
        }
    }
}
