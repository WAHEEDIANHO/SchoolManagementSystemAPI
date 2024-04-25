using MediatR;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Idempotency
{
    public class IdempotencyCommand: IRequest
    {
        
        public IdempotencyCommand(Guid RequestId, RegisterRequestDTO registerRequest) {
            this.RequestId = RequestId;
            this.registerRequest = registerRequest;
        }

        public Guid RequestId { get; set; }
        public RegisterRequestDTO registerRequest {  get; set; }
    }
}
