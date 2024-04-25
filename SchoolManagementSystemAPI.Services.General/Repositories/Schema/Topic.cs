using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{

    public class Topic
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TopicId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string TopicName { get; set; } = string.Empty;
        [Required]
        public int Term { get; set; }
        [Required]
        public int GradeSubjectGradeNumber { get; set; }
        [Required]
        public string GradeSubjectSubjectTitle { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        
        public ICollection<Lesson>? Lessons { get; set; }
        
        //[ForeignKey("GradeSubjectId")]
        public GradeSubject? GradeSubject { get; set; }
        public ICollection<Webinar> Webinars { get; set; }
    }
}
