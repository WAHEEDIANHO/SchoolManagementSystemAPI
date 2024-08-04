namespace SchoolManagementSystemAPI.Services.General.Model.Dto;

public class NoteResDto
{
    public string NoteId { get; set; }
    public string StudentId { get; set; }
    public string LessonId { get; set; }
    public string NoteContent { get; set; }
    public int GradeSubjectGradeNumber { get; set; }
    public string GradeSubjectSubjectTitle { get; set; }
}