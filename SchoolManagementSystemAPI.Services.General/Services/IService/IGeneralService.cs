namespace SchoolManagementSystemAPI.Services.General.Services.IService;

public interface IGeneralService<TDto, TResDto, TSchema>  where TDto: class where TSchema: class where TResDto: class// T stand for Dto and U stand for Schema
{
    Task<IEnumerable<TResDto>> GetAll(Dictionary<string, string>? queries = null);
    Task<TResDto> GetById(string id);
    Task<bool> Delete(string id);
    Task<bool> Add(TDto service);  
}