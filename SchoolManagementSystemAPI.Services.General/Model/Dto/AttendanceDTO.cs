using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class AttendanceDTO
    {
        public AttendanceHeaderDTO AttendanceHeader { get; set; } = new();
        public IEnumerable<AttendanceDetailDTO> AttendanceDetails { get; set; } = Enumerable.Empty<AttendanceDetailDTO>();
    }
}
