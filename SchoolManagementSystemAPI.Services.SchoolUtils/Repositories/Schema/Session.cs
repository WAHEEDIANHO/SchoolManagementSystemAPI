using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema
{
    public class Session
    {
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SessionName { get; set; }
    }
}
