using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Repositories
{
    public class ApplicationUser: IdentityUser
    {
        public  string LastName { get; set; }
        public  string FirstName { get; set; }
        public  string OtherName { get; set; }
        public string? StateOfOrigin { get; set; }
        public string? LocalGovArea { get; set; }
        public  string HomeAddress { get; set; }
        public  string Role { get; set; }
        public  string Gender { get; set; }
        public  DateTime DateofBirth { get; set; }
        public string? BloodGroup { get; set; }
        public string? Religion { get; set; }
        //public required string? imgUrl { get; set; }

        [NotMapped] 
        public IFormFile? Profile { get; set; }
    }


}
