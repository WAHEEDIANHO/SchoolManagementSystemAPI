namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{ 
    public class RegisterRequestDTO
    {
        public required string LastName {  get; set; }       
        public required string FirstName {  get; set; }
        public required string OtherName {  get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string PhoneNumber { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? LocalGovArea { get; set; }
        public required string HomeAddress { get; set; }
        public required string Role { get; set; }
        public required string  Gender { get; set; }
        public required DateTime DateofBirth { get; set; }
        public string? BloodGroup { get; set; }
        public string? Religion { get; set; }
        public IFormFile? Profile { get; set; }

    }
}
