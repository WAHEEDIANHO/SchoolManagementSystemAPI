using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService
{
    public interface IClassSubjectService
    { 

        Task<bool> EnrolSubjectTOClass(ClassSubjectDTO classSubject); 

        IEnumerable<ClassSubjectDTO> GetClassSubjects(int GradeNumber); 
        Task<ClassSubjectDTO> GetSingleClassSubject(int GradeNumber, string SubjectTitle); 
        Task<bool> UnEnrolSubjectTOClass(int GradeNumber, string SubjectTitle); 
    }
}
