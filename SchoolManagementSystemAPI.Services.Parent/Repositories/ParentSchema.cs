using System.ComponentModel.DataAnnotations;

namespace SchoolManagementSystemAPI.Services.Parent.Repositories
{
    public class ParentSchema
    {
        [Key]
        public string RegId { get; set; }
        public string Occupation { get; set; }
    }
}
