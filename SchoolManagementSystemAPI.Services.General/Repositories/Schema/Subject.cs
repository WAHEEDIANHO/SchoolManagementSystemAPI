using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubjectTitle { get; set; }
        //[Required]
        //public string scode { get; set; }
        private IEnumerable<Grade> Grades { get; set; } = Enumerable.Empty<Grade>();
    }
}
