using Newtonsoft.Json;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Parent.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<IEnumerable<UserResponseDTO>> GetParent()
        {
            try
            {
                HttpClient client = _clientFactory.CreateClient("UserAPI");
                var response = await client.GetAsync($"api/user?by-role=teacher");
                var apiContent = await response.Content.ReadAsStringAsync();   
                var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

                if(resp.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<IEnumerable<UserResponseDTO>>(Convert.ToString(resp.Result));
                }else return Enumerable.Empty<UserResponseDTO>();
            }catch (Exception ex) { return Enumerable.Empty<UserResponseDTO>(); }
        }

        public async Task<UserResponseDTO> GetParentById(string id)
        {
            try
            {
                HttpClient client = _clientFactory.CreateClient("UserAPI");
                var response = await client.GetAsync($"api/user/{id}");
                var apiContent = await response.Content.ReadAsStringAsync(); 
                var resp = JsonConvert.DeserializeObject<ResponseDTO>(apiContent);

                if (resp.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<UserResponseDTO>(Convert.ToString(resp.Result));
                }
                else return new();
            }catch(Exception ex) { return new UserResponseDTO(); }
        }
    }
}
