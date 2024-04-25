using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema
{
    public class Session
    {
        public DateTime SessionStartDate { get; set; }
        public DateTime SessionEndDate { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string SessionName { get; set; }

        public ICollection<Event?> Events;
    }
}
