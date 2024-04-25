using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    [PrimaryKey("AttendanceDate", "UserId")]
    public class AttendanceDetail
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
        [Required]
        public string AttendanceHeaderId { get; set; } = string.Empty;
        [Required]
        public string AttendanceDate { get; set; } = DateOnly.FromDateTime(DateTime.Now).ToString();
        [Required]
        public string AttendanceTimeIn { get; set; } = string.Format("" + DateTime.Now.Hour, ":", DateTime.Now.Minute);
        public string? AttendanceTimeOut { get; set; } = string.Empty;
        public string UserId { get; set;}  = string.Empty;
        [Required]
        public string UserRole { get; set; } =  string.Empty;
        
        [ForeignKey("AttendanceHeaderId")]
        public AttendanceHeader AttendanceHeader { get; set; }
    }
}
