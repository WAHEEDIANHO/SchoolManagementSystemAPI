
namespace SchoolManagementSystemAPI.Services.Parent.Model.DTOs
{
    public class UserResponseDTO
    {
        public UserResponseDTO() { }
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string OtherName { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? LocalGovArea { get; set; }
        public string HomeAddress { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public DateTime DateofBirth { get; set; }
        public string? BloodGroup { get; set; }
        public string? Religion { get; set; }
        //public required string? imgUrl { get; set; }
        public UserResponseDTO(UserResponseDTO user) {
            Id = user.Id;
            BloodGroup = user.BloodGroup;
            LocalGovArea = user.LocalGovArea;
            Email= user.Email;
            UserName= user.UserName;
            PhoneNumber= user.PhoneNumber;
            LastName= user.LastName;
            FirstName= user.FirstName;
            OtherName= user.OtherName;
            StateOfOrigin = user.StateOfOrigin;
            HomeAddress = user.HomeAddress;
            Role = user.Role;
            Gender = user.Gender;
            DateofBirth = user.DateofBirth;
            Religion = user.Religion;
        }

        

    }
}
