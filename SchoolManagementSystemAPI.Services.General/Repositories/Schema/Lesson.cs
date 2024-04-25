using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    public class Lesson
    {
        [Key]
        public string LessonId { get; set; } = Guid.NewGuid().ToString();
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Objectives { get; set; } = string.Empty;
        [Required]
        public string Transcript { get; set; } = string.Empty;
        [Required]
        public string MediaUrl { get; set; } = string.Empty;
        [NotMapped]
        public IFormFile Media { get; set; }
        [Required] 
        public string TopicId { get; set; } = string.Empty;
        
        [ForeignKey("TopicId")]
        public Topic Topic { get; set; }
    }
}