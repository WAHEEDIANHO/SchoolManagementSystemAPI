using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SubjectTitle { get; set; }
        //[Required]
        //public string scode { get; set; }
    }
}
