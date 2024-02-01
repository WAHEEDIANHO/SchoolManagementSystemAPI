using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.AuthAPI.Model.DTOs;
using SchoolManagementSystemAPI.Services.AuthAPI.Services.IServices;

namespace SchoolManagementSystemAPI.Services.AuthAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService<RegisterRequestDTO> _authService;
        private readonly ResponseDTO _responseDTO;
        public AuthController(IAuthService<RegisterRequestDTO> authService) { 
            _authService = authService;
            _responseDTO = new ResponseDTO();
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDTO>> Login(LoginRequestDTO loginRequest)
        {
            try
            {
                var loginCredential = await _authService.Login(loginRequest);
                if (loginCredential == null)
                {
                    return BadRequest(new LoginResponseDTO()
                    {
                        token = "",
                        user = new()
                    });
                }
                return Ok(loginCredential);
            }catch (Exception ex)
            {
                return StatusCode(500, ex.ToString()); 
            }
        }


       /* [HttpPost("register")]
        public async Task<ActionResult<ResponseDTO>> Register(RegisterRequestDTO registerRequest)
        {
            try
            {
                bool isRegister = await _authService.Register(registerRequest);
                if (!isRegister)
                {
                    _responseDTO.Result = null;
                    _responseDTO.message = "Unble to register user";
                    _responseDTO.IsSuccessful = false;

                    return BadRequest(_responseDTO);
                }

                _responseDTO.Result = "Success";
                _responseDTO.message = string.Empty;
                _responseDTO.IsSuccessful = true;
                return Ok(_responseDTO);
            }
            catch (Exception ex)
            {
                if(ex is BadHttpRequestException) { return BadRequest(ex.Message); }
                return StatusCode(500, ex.Message);
            }
        }*/
    }
}
