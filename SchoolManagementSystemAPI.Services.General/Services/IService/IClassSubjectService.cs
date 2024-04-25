using SchoolManagementSystemAPI.Services.General.Model.Dto;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public interface IClassSubjectService
    { 

        Task<bool> EnrolSubjectTOClass(ClassSubjectDTO classSubject); 

        IEnumerable<ClassSubjectDTO> GetClassSubjects(int GradeNumber); 
        Task<ClassSubjectDTO> GetSingleClassSubject(int GradeNumber, string SubjectTitle); 
        Task<bool> UnEnrolSubjectTOClass(int GradeNumber, string SubjectTitle); 
    }
}
