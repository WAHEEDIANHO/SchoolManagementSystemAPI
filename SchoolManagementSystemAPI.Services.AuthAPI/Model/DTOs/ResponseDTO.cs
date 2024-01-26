namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{
    public class ResponseDTO
    {
        public object? Result { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public string message { get; set; } = "";
    }
}
