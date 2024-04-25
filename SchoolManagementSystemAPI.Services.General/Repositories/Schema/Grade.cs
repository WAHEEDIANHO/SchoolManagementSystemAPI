using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    public class Grade
    {
        [Required] 
        public string GradeName { get; set; } = string.Empty;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GradeNumber { get; set; }
        public string? GradeTeacher { get; set; }

        private IEnumerable<Subject> Subjects { get; set; } = Enumerable.Empty<Subject>();
    }
}
