namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{
    public class LoginResponseDTO
    {
        public UserDTO user { get; set; }
        public string token { get; set; }
    }
}
