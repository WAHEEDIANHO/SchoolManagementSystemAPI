using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    //[PrimaryKey("GradeNumber", "SessionName", "Term")]
    public class AttendanceHeader
    {
        [Key]
        public string AttendanceHeaderId { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("GradeNumber")]
        public int GradeNumber { get; set; }
        [ForeignKey("SessionName")]
        public string SessionName { get; set; }
        [Required]
        public int Term { get; set; } = 0;
        [Required]
        public int TotalExpectedAttendance { get; set; } = 0;

        public ICollection<AttendanceDetail> AttendanceDetails { get; set; }
    }
}
