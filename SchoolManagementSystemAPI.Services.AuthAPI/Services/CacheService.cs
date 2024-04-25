using Newtonsoft.Json;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;
using StackExchange.Redis;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Services;
public class CacheService : ICacheService
{
    private IDatabase _cacheDb;

    public CacheService()
    {
        var redis = ConnectionMultiplexer.Connect("localhost:6379");
        _cacheDb = redis.GetDatabase();
    }
    public T GetData<T>(string key)
    {
        var value = _cacheDb.StringGet(key);
        if (!string.IsNullOrEmpty(value)) return JsonConvert.DeserializeObject<T>(value);

        return default;
    }

    public object RemoveData<T>(string key)
    {
        bool isExist = _cacheDb.KeyExists(key);
        if (isExist) return _cacheDb.KeyDelete(key);
        return false;
    }

    public bool SetData<T>(string key, T value, DateTimeOffset expirationTIme)
    {
        var expiryDate = expirationTIme.DateTime.Subtract(DateTime.Now);
        string val = JsonConvert.SerializeObject(value);
        return _cacheDb.StringSet(key, val, expiryDate);
    }
}
