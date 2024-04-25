using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.General.Model.Dto
{
    public class AttendanceDetailDTO
    {
        public string AttendanceHeaderId { get; set; } = string.Empty;
        public string AttendanceDate { get; set; } = string.Empty;
        public string AttendanceTimeIn { get; set; } = string.Empty;
        public string? AttendanceTimeOut { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserRole { get; set; } = string.Empty;
        
        //public AttendanceHeaderDTO? AttendanceHeader { get; set; }
    }
}
