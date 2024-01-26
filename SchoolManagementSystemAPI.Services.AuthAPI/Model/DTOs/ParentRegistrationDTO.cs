namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{
    public class ParentRegistrationDTO: RegisterRequestDTO
    {
        public required string Occupation { get; set; }
    }
}
