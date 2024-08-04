using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _service;
        private readonly ResponseDTO _response;

        public GradeController(IGradeService service)
        {
            _service = service;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllAvailableGrade()
        {
            try
            {
                _response.Result = await _service.GetAllClass();
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> CreateGrade([FromBody] GradeDTO stdClassDTO)
        {
            try
            {
                await _service.AddClass(stdClassDTO);
                _response.Result = "Successfully created the class";
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.ToString();
                return BadRequest(_response);
            }
        }

        [HttpGet("{GradeNumber}")]
        public async Task<ActionResult<ResponseDTO>> GetGradeById(int GradeNumber)
        {
            try
            {
                _response.Result = await _service.GetClassByKey(GradeNumber);
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.ToString();
                return StatusCode(500, _response);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ResponseDTO>> UpdateGradeTeacher([FromBody] GradeDTO update)
        {
            try
            {
                if(await _service.UpdateClassTeacher(update))
                {
                    _response.Result = "Grade Teacher updated!!!";
                    return Ok(_response);
                }
                _response.IsSuccessful = false;
                _response.message = "Error updating Teacher grade retry!!!";
                return BadRequest(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.ToString();
                return StatusCode(500, _response);
            }
        }

        [HttpDelete]
        public async Task<ActionResult<ResponseDTO>> DeleteGrade(int GradeNumber)
        {
            try
            {
                if(await _service.DeleteClassbyId(GradeNumber))
                {
                    _response.Result = "Deleted successfully";
                    return Ok(_response);
                }
                _response.IsSuccessful = false;
                _response.message = "unable to delete Grade";
                return BadRequest(_response);
            }catch(Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.ToString();
                return StatusCode(500, _response);
            }
        }
    }
}
