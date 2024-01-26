using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.Dto;
using SchoolManagementSystemAPI.Services.SchoolUtils.Model.DTOs;
using SchoolManagementSystemAPI.Services.SchoolUtils.Services.IService;

namespace SchoolManagementSystemAPI.Services.SchoolUtils.Controllers
{
    [Route("class-subject")]
    public class ClassSubjectController : Controller
    {
        private readonly IClassSubjectService _service;
        private readonly ResponseDTO _resp;

        public ClassSubjectController(IClassSubjectService service)
        {
            _service = service;
            _resp = new ResponseDTO();
        }

        [HttpPost]
        public async Task<IActionResult> AddSujectToClass([FromBody] ClassSubjectDTO classSubject)
        {
            try
            {
                if(await _service.EnrolSubjectTOClass(classSubject))
                {
                    _resp.Result = "Success";
                    return Ok(_resp);
                }
                _resp.IsSuccessful = false;
                _resp.message = "unable to perform action";
                return BadRequest(_resp);
            }
            catch (Exception ex)
            {
                _resp.IsSuccessful = false;
                _resp.message = ex.ToString();
                return StatusCode(500, _resp);
            }
        }

        [HttpGet("{GradeNumber}")]
        public  ActionResult<IEnumerable<ClassSubjectDTO>> GetClassSubject(int GradeNumber)
        {
            try
            {
                _resp.Result =  _service.GetClassSubjects((int)GradeNumber);
                return Ok(_resp);
            }catch (Exception ex)
            {
                _resp.IsSuccessful = false;
                _resp.message = ex.ToString();
                return StatusCode(500, _resp);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSingleClassSubject([FromQuery] int GradeNumber, [FromQuery] string SubjectTitle)
        {
            try
            {
              _resp.Result = await _service.GetSingleClassSubject((int) GradeNumber, SubjectTitle);
                return Ok(_resp);
            }catch(Exception ex) { 
                _resp.IsSuccessful = false;
                _resp.message = ex.ToString();
                return StatusCode(500, _resp);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOneClassSubject([FromQuery] int GradeNumber, [FromQuery] string SubjectTitle)
        {
            try
            {
               if(await _service.UnEnrolSubjectTOClass((int)GradeNumber, SubjectTitle))
                {
                    _resp.Result = "Deleted successsfully";
                    return Ok(_resp);
                }
               _resp.IsSuccessful = false;
                _resp.message = "Unable to delete";
                return BadRequest(_resp);
            }catch (Exception ex)
            {
                _resp.IsSuccessful = false;
                _resp.message = ex.ToString();
                return StatusCode(500, _resp);
            }
        }
    }
}
