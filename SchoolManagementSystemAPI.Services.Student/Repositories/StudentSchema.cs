using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.Student.Repositories
{
    public class StudentSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required string AdmissionNo { get; set; }
        [Required]
        public required int ClassId { get; set; }
        [Required]
        public required string SessionId { get; set; }
        [Required]
        public required string RegId { get; set; }


    }
}
