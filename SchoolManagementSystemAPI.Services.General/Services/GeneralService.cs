using AutoMapper;
using GenericRepository;
using SchoolManagementSystemAPI.Services.General.Repositories.Data;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Services;
 
public class DefaultType
{
    // This is the default type that will be used if no type is specified
}
public class GeneralService<TReqDto, TResDto, TSchema>: IGeneralService<TReqDto, TResDto, TSchema> where TReqDto: class where TResDto: class where TSchema: class // T stand for Dto and U stand for Schema
{
    private readonly IGenericRepository<TSchema, AppDbContext> _repository;
    private readonly IMapper _mapper;

    public GeneralService(IMapper mapper, IGenericRepository<TSchema, AppDbContext> repository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TResDto>> GetAll(Dictionary<string, string>? queries = null)
    {
        return _mapper.Map<IEnumerable<TResDto>>(await _repository.GetAll(queries));
    }

    public async Task<TResDto> GetById(string id)
    {
        return _mapper.Map<TResDto>(await _repository.GetByKey(id));
    }

    public async Task<bool> Delete(string id)
    {
        TSchema item = await _repository.GetByKey(id);
        if (item != null)
        {
            _repository.Delete(item);
            return true;
        }

        return false;
    }

    public async Task<bool> Add(TReqDto service)
    {
        TSchema itemToAdd = _mapper.Map<TSchema>(service);
        await _repository.Add(itemToAdd);
        return true;
    }
}