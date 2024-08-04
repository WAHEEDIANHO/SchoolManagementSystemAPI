using AutoMapper;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.General.Services.IService
{
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _stdCLassRepo;
        private readonly IMapper _mapper;

        public GradeService(IGradeRepository stdCLassRepo, IMapper mapper)
        {
            _stdCLassRepo = stdCLassRepo;
            _mapper = mapper;

        }

        public async Task<bool> AddClass(GradeDTO stdClassDTO)
        {
            try
            {
                Grade reg = _mapper.Map<Grade>(stdClassDTO);
                await _stdCLassRepo.Add(reg);
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<bool> DeleteClassbyId(int id)
        {
            try
            {
                var res = await _stdCLassRepo.GetById(id);
                _stdCLassRepo.Delete(res);
                return true;
            }
            catch (Exception e) { throw; }
        }

        public async Task<bool> UpdateClassTeacher(GradeDTO update)
        {
            try
            {
                var res = await _stdCLassRepo.GetById(update.GradeNumber);
                if(res != null && update.GradeTeacher != null)
                {
                    res.GradeTeacher = update.GradeTeacher;
                    _stdCLassRepo.Update(res);
                    return true;
                }
                return false;
            }
            catch (Exception e) { throw; }
        }

        public async Task<IEnumerable<GradeDTO>> GetAllClass(Dictionary<string, string>? filter = null)
        {
            try
            {
                var result = await _stdCLassRepo.GetAll(filter);
                return _mapper.Map<IEnumerable<GradeDTO>>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<GradeDTO> GetClassByKey(int GradeNumber)
        {
            try
            {
                var res = await _stdCLassRepo.GetById(GradeNumber);
                return _mapper.Map<GradeDTO>(res);
            }
            catch (Exception e) { throw; }
        }
    }
}
