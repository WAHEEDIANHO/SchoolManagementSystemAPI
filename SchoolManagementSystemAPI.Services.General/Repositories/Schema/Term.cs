using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Repositories.Schema;

[PrimaryKey("SessionName", "TermNumber")]
public class Term
{
    [Required]
    public string SessionName { get; set; }
    [Required]
    public DateTime TermStartDate { get; set; }
    [Required]
    public string TermName { get; set; }
    public DateTime TermEndDate { get; set; }
    [Required]
    public int TermNumber { get; set; }
    [Required] 
    public TermStatus TermStatus { get; set; } = TermStatus.Ongoing; // "ongoing", "completed"
    
    
    [ForeignKey("SessionName")]
    public Session Session { get; set; }
    public ICollection<Topic>? Topics { get; set;  }
}

public enum TermStatus
{
    Ongoing,
    Completed
}