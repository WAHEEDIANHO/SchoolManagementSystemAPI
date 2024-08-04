using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectServices _services;
        private readonly ResponseDTO _response;

        public SubjectController(ISubjectServices services)
        {
            _services = services;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllAvailableSubject()
        {
            try
            {
               _response.Result = await _services.GetAllSubjects();
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateSubject([FromBody] SubjectRequestDTO subject)
        {
            try
            {
                await _services.CreateSubject(subject);
                _response.Result = "Successfully created subject";
;                return Ok(_response); ;
            }catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.ToString();
                return BadRequest(_response);
            }
        }

       [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetBySubjectId([FromQuery] string id)
        {
            try {
                _response.Result = await _services.GetSubjectByID(id);
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeleteSubject([FromQuery] string id)
        {
            try
            {
                _response.Result = await _services.DeleteSubjectByID(id);
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
