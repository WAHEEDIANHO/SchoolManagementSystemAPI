using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Repositories
{
    public sealed class IdempotencySchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedOnUTC { get; set; }
        [Required]
        public int StatusCode { get; set; }
        [Required]
        public string Response { get; set; }
    }
}
