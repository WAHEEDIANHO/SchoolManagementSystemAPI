using AutoMapper;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.Schema;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService
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

        public async Task<bool> deleteClassbyId(int id)
        {
            try
            {
                var res = await _stdCLassRepo.GetById(id);
                _stdCLassRepo.Delete(res);
                return true;
            }
            catch (Exception e) { throw; }
        }

        public async Task<bool> updateClassTeacher(GradeDTO update)
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

        public async Task<IEnumerable<GradeDTO>> getAllClass()
        {
            try
            {
                var result = await _stdCLassRepo.GetAll();
                return _mapper.Map<IEnumerable<GradeDTO>>(result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<GradeDTO> getClassByKey(int GradeNumber)
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
