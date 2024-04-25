/*using Azure;
using MediatR;
using SchoolManagementSystemAPI.Services.AuthAPI.Idempotency;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Behaviour
{
    internal sealed class IdempotentCommandPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IdempotencyCommand where TResponse : ResponseDTO
    {
        private readonly IIdenpotencyServices _idenpotencyServices;
        private readonly IAuthService<RegisterRequestDTO> _service;
        private readonly ResponseDTO _response;

        public IdempotentCommandPipelineBehaviour(IAuthService<RegisterRequestDTO> service, IIdenpotencyServices idenpotencyServices)
        {
            _idenpotencyServices = idenpotencyServices;
            _service = service;
            _response = new();
        }
        public async Task<ResponseDTO> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(await _idenpotencyServices.RequestExistAsync(request.RequestId))
            {
                return default;
            }

            bool isRegister = await _service.Register(request.registerRequest);
            if (!isRegister)
            {
                _response.Result = null;
                _response.message = "Unble to register user";
                _response.IsSuccessful = false;

                return _response;
            }

            _response.Result = "Success";
            _response.message = string.Empty;
            _response.IsSuccessful = true;
            await _idenpotencyServices.CreateRequestAsync(request.RequestId, typeof(TRequest).Name);
            var resp = await next();
            return resp;
        }
    }
}
*/