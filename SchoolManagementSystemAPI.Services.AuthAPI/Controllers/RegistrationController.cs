using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Repositories;
using SchoolManagementSystemAPI.Services.AuthAPI.Services;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class RegistrationController : ControllerBase
    {
        private readonly IAuthService<RegisterRequestDTO> _service;
        private readonly ResponseDTO _response;

        public RegistrationController(IAuthService<RegisterRequestDTO> service)
        {
            _service = service;
            _response = new();
        }

        [HttpPost("register/teacher")]
        public async Task<ActionResult<ResponseDTO>> TeacherRegistration([FromForm] TeacherRegisterDTO registerRequest)
        {
            Console.WriteLine("-------> Entering Teacher reg");
            registerRequest.Role = "TEACHER";
            try
            {
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register user";
                    _response.IsSuccessful = false;

                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                if (ex is BadHttpRequestException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex.ToString());
            }
        }

        [HttpPost("register/student")]
        public async Task<ActionResult<ResponseDTO>> StudentRegistration([FromForm] StudentRegisterDTO registerRequest)
        {
            registerRequest.Role = "STUDENT";

            try
            {
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register user";
                    _response.IsSuccessful = false;

                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                if (ex is BadHttpRequestException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("register/parent")]
        public async Task<ActionResult<ResponseDTO>> ParentRegistration([FromForm] ParentRegistrationDTO registerRequest)
        {
            registerRequest.Role = "PARENT";
            try
            {
                bool isRegister = await _service.Register(registerRequest);
                if (!isRegister)
                {
                    _response.Result = null;
                    _response.message = "Unble to register parent";
                    _response.IsSuccessful = false;

                    return BadRequest(_response);
                }

                _response.Result = "Success";
                _response.message = string.Empty;
                _response.IsSuccessful = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                if (ex is BadHttpRequestException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex.Message);
            }
        }

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

        [HttpGet]
        public ActionResult<IEnumerable<ResponseDTO>> GetUsersByRole()
        {
            try
            {
                _response.Result = _service.GetAllUsers();
                return Ok(_response);
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
               
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
    }
}
