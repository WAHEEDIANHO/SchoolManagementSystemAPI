

using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface INoteService: IGeneralService<NoteDto, NoteResDto, Note>
{
    Task<IEnumerable<NoteResDto>> GetStudentPersonalNotes(string studentId, string lessonId);
}