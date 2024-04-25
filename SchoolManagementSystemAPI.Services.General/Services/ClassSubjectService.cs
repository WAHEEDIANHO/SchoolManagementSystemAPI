using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services
{
    public class ClassSubjectService : IClassSubjectService
    {
        private readonly IMapper _mapper;
        private readonly IClassSubjectRepository _repo;
        public ClassSubjectService(IMapper mapper, IClassSubjectRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<bool> EnrolSubjectTOClass(ClassSubjectDTO classSubject)
        {
            try
            {
                GradeSubject gradeSubject1 = _mapper.Map<GradeSubject>(classSubject);
                await _repo.Add(gradeSubject1);
                return true;

                
            }catch (Exception ex) { throw; }
        }

        public IEnumerable<ClassSubjectDTO> GetClassSubjects(int GradeNumber)
        {
            try
            {
               return _mapper.Map<IEnumerable<ClassSubjectDTO>>(_repo.GetClassSubject(GradeNumber));
            }catch (Exception ex) { throw; }
        }

        public async Task<ClassSubjectDTO> GetSingleClassSubject(int GradeNumber, string SubjectTitle)
        {
            try
            {
               var res = await _repo.GetSingleClassSubject(GradeNumber, SubjectTitle);
                return _mapper.Map<ClassSubjectDTO>(res);
            }catch(Exception ex) { throw; }
        }

        public async Task<bool> UnEnrolSubjectTOClass(int GradeNumber, string SubjectTitle)
        {
            try
            {
                var res = await _repo.GetSingleClassSubject(GradeNumber, SubjectTitle);
                if(res != null)
                {
                    _repo.Delete(res);
                    return true;
                }return false;

            }
            catch (Exception ex) { throw; }
        }
    }
}
