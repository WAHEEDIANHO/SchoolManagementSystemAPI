using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.DTOs;
using SchoolManagementSystemAPI.Services.SchoolUtils.Repositories.IRepositories;
using SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Controllers
{
    [ApiController]
    [Route("/api/subject")]
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
        public async Task<ActionResult<ResponseDTO>> GetAll()
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
        public async Task<ActionResult<ResponseDTO>> Create([FromBody] SubjectRequestDTO subject)
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

       [HttpGet("getById")]
        public async Task<ActionResult<ResponseDTO>> GetById([FromQuery] string id)
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
        public async Task<ActionResult<ResponseDTO>> DeleteById([FromQuery] string id)
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
