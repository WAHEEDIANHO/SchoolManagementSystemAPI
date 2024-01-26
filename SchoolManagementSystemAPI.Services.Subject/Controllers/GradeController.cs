using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.DTOs;
using SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Controllers
{
    [Route("/api/stdclass")]
    public class GradeController : Controller
    {
        private readonly IGradeService _service;
        private readonly ResponseDTO _response;

        public GradeController(IGradeService service)
        {
            _service = service;
            _response = new ResponseDTO();
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAll()
        {
            try
            {
                _response.Result = await _service.getAllClass();
                return Ok(_response);
            }
            catch (Exception ex) {
                _response.IsSuccessful = false;
                _response.message = ex.Message;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseDTO>> Create([FromBody] GradeDTO stdClassDTO)
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

        [HttpGet("getById/{GradeNumber}")]
        public async Task<ActionResult<ResponseDTO>> GetById(int GradeNumber)
        {
            try
            {
                _response.Result = await _service.getClassByKey(GradeNumber);
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
                if(await _service.updateClassTeacher(update))
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
        public async Task<ActionResult<ResponseDTO>> Delete(int GradeNumber)
        {
            try
            {
                if(await _service.deleteClassbyId(GradeNumber))
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
