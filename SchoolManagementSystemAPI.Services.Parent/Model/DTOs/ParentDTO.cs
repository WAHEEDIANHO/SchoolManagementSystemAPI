using SchoolManagementSystemAPI.Services.Parent.Repositories;

namespace SchoolManagementSystemAPI.Services.Parent.Model.DTOs
{
    public class ParentDTO: UserResponseDTO
    {
        public ParentDTO(UserResponseDTO user, ParentSchema parent): base(user) {
        
            RegId = parent.RegId;
            Occupation= parent.Occupation;
        }
        public ParentDTO() { }
        public string RegId { get; set; }
        public string Occupation { get; set; }
    }
}
