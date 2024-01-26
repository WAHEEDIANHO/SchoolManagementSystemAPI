namespace SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs
{
    public class MsgRegStudentDTO
    {
        public required string AdmissionNo { get; set; }
        public required int ClassId { get; set; }
        public required int SessionId { get; set; }
        public required string RegId { get; set; }
    }
}
