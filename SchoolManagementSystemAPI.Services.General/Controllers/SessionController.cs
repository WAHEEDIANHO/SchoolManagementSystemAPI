using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Model
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SessionController : ControllerBase
    {
        private readonly ISessionService _service;
        private readonly ResponseDTO _response;

        public SessionController(ISessionService service)
        {
            _service = service;
            _response = new ResponseDTO();  
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllAcademicSession()
        {
            try
            {
                _response.Result = await _service.getAllClass();
                return Ok(_response);
            }
            catch (Exception ex) { 
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response); }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateAcademicSession([FromBody] SessionDTO schSessionDTO)
        {
            try
            {
                await _service.AddClass(schSessionDTO);
                _response.Result = "Successfully created Session Successfully";
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.ToString();
                return BadRequest(_response);
            }
        }

        [HttpGet("getById")]
        public async Task<ActionResult<ResponseDTO>> GetAcademicSessionById([FromQuery] string id)
        {
            try
            {
                _response.Result = await _service.getSessionById(id);
                return  Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeleteAcademicSession([FromQuery] string id)
        {
            try
            {
                _response.Result = await _service.deleteSessionbyId(id);
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response);
            }
        }
    }
}
