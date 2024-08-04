using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services
{
    public class SubjectService : ISubjectServices
    {
        private readonly ISubjectRepository _subRepository;
        private readonly IMapper _mapper;

        public SubjectService(ISubjectRepository subRepository, IMapper mapper)
        {
            _subRepository = subRepository;
            _mapper = mapper;
        }
        public async Task<bool> CreateSubject(SubjectRequestDTO req)
        {
            try
            {
                req.SubjectTitle = req.SubjectTitle.ToUpper();
               Subject regSubject = _mapper.Map<Subject>(req);
               await _subRepository.Add(regSubject);
                return true;
            }catch(Exception e)
            {
                throw;
            }
        }

        public async Task<IEnumerable<SubjectResponseDTO>> GetAllSubjects(Dictionary<string, string>? filter = null)
        {
            try
            {
                var result = await _subRepository.GetAll(filter);
                return _mapper.Map<IEnumerable<SubjectResponseDTO>>(result);
            }
            catch(Exception e) {
               throw;
            }
        }

        public async Task<SubjectResponseDTO> GetSubjectByID(string id)
        {
            try
            {
              var res = await  _subRepository.GetByKey(id);
               return _mapper.Map<SubjectResponseDTO>(res);
            }catch(Exception e) { throw; }
        }

        public async Task<SubjectResponseDTO> DeleteSubjectByID(string id)
        {
            try
            {
                var res = await _subRepository.GetByKey(id);
                _subRepository.Delete(res);
                return _mapper.Map<SubjectResponseDTO>(res);
            }
            catch (Exception e) { throw; }
        }

        public async Task<SubjectResponseDTO> Update(string id)
        {
            try
            {
                var res = await _subRepository.GetByKey(id);
                _subRepository.Update(res);
                return _mapper.Map<SubjectResponseDTO>(res);
            }
            catch (Exception e) { throw; }
        }
    }
}
