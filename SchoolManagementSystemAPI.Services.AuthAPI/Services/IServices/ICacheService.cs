namespace SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;
public interface ICacheService
{
    T GetData<T>(string key);

    bool SetData<T>(string key, T value, DateTimeOffset expirationTIme);

    Object RemoveData<T>(string key);
}
