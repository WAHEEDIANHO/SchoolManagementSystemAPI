using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Idempotency
{
    public interface IIdenpotencyServices
    {
        Task<bool> RequestExistAsync (Guid requestId);

        Task CreateRequestAsync (Guid requestId, int statusCode, string name, ResponseDTO response);
        Task<IdempotencySchema> GetRequestResponse (Guid requestId);
    }
}
