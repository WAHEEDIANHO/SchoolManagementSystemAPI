
namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{
    public class StudentRegisterDTO: RegisterRequestDTO
    {
        public required int ClassId { get; set; }
        public required int SessionId { get; set; }
    }
}
