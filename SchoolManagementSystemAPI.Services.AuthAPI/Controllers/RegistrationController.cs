using System.Security.Claims;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchoolManagementSystemAPI.Services.AuthAPI.Idempotency;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Services;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Controllers
{
   // [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/user")]
    public sealed class RegistrationController : ControllerBase
    {
        private readonly IAuthService<RegisterRequestDTO> _service;
        private readonly ResponseDTO _response;
        //private readonly IMediator _mediator;
        private readonly IIdenpotencyServices _idempotencyServices;
        private int statusCode = 200;

        public RegistrationController(IAuthService<RegisterRequestDTO> service, IIdenpotencyServices idenpotencyServices)
        {
            _service = service;
            _response = new();
            //_mediator = mediator;
            _idempotencyServices = idenpotencyServices;
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public ActionResult<IEnumerable<ResponseDTO>> GetAllUsers()
        {
            try
            {
                _response.Result = _service.GetAllUsers();
                return Ok(_response);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [Authorize(Roles = "ADMIN,TEACHER")]
        [HttpGet("by-role")]
        public ActionResult<IEnumerable<ResponseDTO>> GetUsersByRole([FromQuery] string role)
        {
            try
            {
                _response.Result = _service.GetUsersByRole(role);
                return Ok(_response);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDTO>> GetUser(string userId)
        {
            try
            {
                var res = await _service.GetUser(userId);
                if (res == null)
                {
                    _response.Result = new object();
                    _response.message = "no user found with the given name";
                }
                else
                {
                    _response.Result = res;
                }
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.IsSuccessful = false;
                if (e is InvalidOperationException)
                {
                    _response.message = "user not found!";
                    return NotFound(_response);
                }

                return StatusCode(500, e.Message);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost("create-role")]
        public async Task<ActionResult<ResponseDTO>> CreateRole([FromQuery] string role)
        {
            try
            {
                return Ok(_service.CreateRole(role));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> AddSystemUser([FromHeader(Name = "X-Idempotency-Key")] string requestId, [FromForm] RegisterRequestDTO registerRequest)
        {
            Console.WriteLine("-------> Entering Teacher reg");
            registerRequest.Role = registerRequest.Role;
            if (!Guid.TryParse(requestId, out Guid parsedRequestId))
            {
                _response.IsSuccessful = false;
                _response.message = "Invalid X-Idempotency-Key";
                return BadRequest(_response);
            }
            try
            {
                //IdempotencyCommand idemCmd = new IdempotencyCommand(parsedRequestId, registerRequest);
                //await _mediator.Send(idemCmd);

                if (await _idempotencyServices.RequestExistAsync(parsedRequestId))
                {
                    //return previous response
                    //return StatusCode(code, response);
                    var res = await _idempotencyServices.GetRequestResponse(parsedRequestId);
                    ResponseDTO resp = JsonConvert.DeserializeObject<ResponseDTO>(res.Response);
                    return StatusCode(res.StatusCode, resp);
                }
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register user";
                    _response.IsSuccessful = false;
                    statusCode = 400;
                    await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                statusCode = 201;
                await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                return Created(typeof(RegistrationController).Name, _response);
            }
            catch (Exception ex)
            {
                statusCode = 500;
                _response.Result = ex.Message;
                _response.IsSuccessful = false;
                if (ex is BadHttpRequestException) { return BadRequest(_response); }
                return StatusCode(statusCode, _response);
            }
        }


        [HttpPost("register/teacher")]
        public async Task<ActionResult<ResponseDTO>> TeacherRegistration([FromHeader(Name = "X-Idempotency-Key")] string requestId, [FromForm] TeacherRegisterDTO registerRequest)
        {
            Console.WriteLine("-------> Entering Teacher reg");
            registerRequest.Role = "TEACHER";
            if (!Guid.TryParse(requestId, out Guid parsedRequestId))
            {
                _response.IsSuccessful = false;
                _response.message = "Invalid X-Idempotency-Key";
                return BadRequest(_response);
            }
            try
            {
                if (await _idempotencyServices.RequestExistAsync(parsedRequestId))
                {
                    //return previous response
                    //return StatusCode(code, response);
                    var res = await _idempotencyServices.GetRequestResponse(parsedRequestId);
                    ResponseDTO resp = JsonConvert.DeserializeObject<ResponseDTO>(res.Response);
                    return StatusCode(res.StatusCode, resp);
                }
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register user";
                    _response.IsSuccessful = false;
                    statusCode = 400;
                    await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                statusCode = 201;
                await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                return Created(typeof(RegistrationController).Name, _response);
            }
            catch (Exception ex)
            {
                statusCode = 500;
                _response.Result = ex.Message;
                _response.IsSuccessful = false;
                if (ex is BadHttpRequestException) { return BadRequest(_response); }
                return StatusCode(statusCode, _response);
            }
        }

        [HttpPost("register/student")]
        public async Task<ActionResult<ResponseDTO>> StudentRegistration([FromHeader(Name = "X-Idempotency-Key")] string requestId, [FromForm] StudentRegisterDTO registerRequest)
        {
            registerRequest.Role = "STUDENT";
            if (!Guid.TryParse(requestId, out Guid parsedRequestId))
            {
                _response.IsSuccessful = false;
                _response.message = "Invalid X-Idempotency-Key";
                return BadRequest(_response);
            }
            try
            {
                if (await _idempotencyServices.RequestExistAsync(parsedRequestId))
                {
                    //return previous response
                    //return StatusCode(code, response);
                    var res = await _idempotencyServices.GetRequestResponse(parsedRequestId);
                    ResponseDTO resp = JsonConvert.DeserializeObject<ResponseDTO>(res.Response);
                    return StatusCode(res.StatusCode, resp);
                }
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register user";
                    _response.IsSuccessful = false;
                    statusCode = 400;
                    await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                statusCode = 201;
                await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                return Created(typeof(RegistrationController).Name, _response);
            }
            catch (Exception ex)
            {
                statusCode = 500;
                _response.Result = ex.Message;
                _response.IsSuccessful = false;
                if (ex is BadHttpRequestException) { return BadRequest(_response); }
                return StatusCode(statusCode, _response);
            }
        }


        [HttpPost("register/parent")]
        public async Task<ActionResult<ResponseDTO>> ParentRegistration([FromHeader(Name = "X-Idempotency-Key")] string requestId, [FromForm] ParentRegistrationDTO registerRequest)
        {
            registerRequest.Role = "PARENT";
            if (!Guid.TryParse(requestId, out Guid parsedRequestId))
            {
                _response.IsSuccessful = false;
                _response.message = "Invalid X-Idempotency-Key";
                return BadRequest(_response);
            }
            try
            {
                if (await _idempotencyServices.RequestExistAsync(parsedRequestId))
                {
                    //return previous response
                    //return StatusCode(code, response);
                    var res = await _idempotencyServices.GetRequestResponse(parsedRequestId);
                    ResponseDTO resp = JsonConvert.DeserializeObject<ResponseDTO>(res.Response);
                    return StatusCode(res.StatusCode, resp);
                }
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register user";
                    _response.IsSuccessful = false;
                    statusCode = 400;
                    await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                statusCode = 201;
                await _idempotencyServices.CreateRequestAsync(parsedRequestId, statusCode, typeof(RegistrationController).Name, _response);
                return Created(typeof(RegistrationController).Name, _response);
            }
            catch (Exception ex)
            {
                statusCode = 500;
                _response.Result = ex.Message;
                _response.IsSuccessful = false;
                if (ex is BadHttpRequestException) { return BadRequest(_response); }
                return StatusCode(statusCode, _response);
            }
        }


    }
}
