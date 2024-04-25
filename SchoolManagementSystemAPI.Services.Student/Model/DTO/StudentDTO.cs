using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Repositories;

namespace SchoolManagementSystemAPI.Services.Student.Model.DTO
{
    public class StudentDTO: UserResponseDTO
    {
        public StudentDTO(UserResponseDTO user, StudentSchema std): base(user) {

            AdmissionNo = std.AdmissionNo;
            ClassId = std.ClassId;
            SessionId = std.SessionId;
        }
        public StudentDTO() { }
        public  string AdmissionNo { get; set; }
        public  int ClassId { get; set; }
        public  string SessionId { get; set; }
        //public required string RegId { get; set; }
    }
}
