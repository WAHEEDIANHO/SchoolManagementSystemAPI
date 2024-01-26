using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema
{
    public class Grade
    {
        [Required]
        public string GradeName { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GradeNumber { get; set; }
        public string? GradeTeacher { get; set; }
    }
}
