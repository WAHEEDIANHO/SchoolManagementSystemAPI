using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Services.IServices;
using SchoolManagementSystemAPI.Services.Student.Utils;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

namespace SchoolManagementSystemAPI.Services.Student.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ResponseDTO response;
        private readonly IGrpcGradeSubjectClient _grpcGradeSubjectClient;

        public StudentController(IStudentService service, IGrpcGradeSubjectClient grpcGradeSubjectClient)
        {
            _service = service;
            response = new ResponseDTO();
            _grpcGradeSubjectClient = grpcGradeSubjectClient;
        }

        [Authorize(Roles = UserRoles.ADMIN)]
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> AllStudent()
        {
            try
            {
                IEnumerable<StudentDTO> students = await _service.GetAllStudent();
                response.Result = students;
                return Ok(response);
            }
            catch (Exception ex) {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = $"{UserRoles.ADMIN }, {UserRoles.TEACHER}")]
        [HttpGet("{gradeId:int}")]
        public ActionResult<ResponseDTO> GetStudentByGrade(int gradeId)
        {
            try
            {
                response.Result = _service.GetByGrade(gradeId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.ToString();
                return StatusCode(500, response);
            }
        }

        // [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> StudentById(string id)
        {
            try
            {
                response.Result = await _service.GetStudentById(id);
                return Ok(response);

            } catch (Exception ex) {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = UserRoles.ADMIN)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(string id)
        {
            try { 
                if(await _service.DeleteStudentById(id))
                 {
                    response.Result = "Deleted successfully";
                    return Ok(response);
                }

                response.IsSuccessful = false;
                response.message = "Unable to delete student";
                return BadRequest(response);
            }catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.ToString();
                return StatusCode(500, response);
            }
        }

        [Authorize(Roles = $"{UserRoles.TEACHER}, {UserRoles.STUDENT}")]
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetGradeSubject([FromQuery]int GradeNumber)
        {
            try
            {
              response.Result = await _grpcGradeSubjectClient.GetClassSubject(GradeNumber); 
              return Ok(response);  
            }catch(Exception ex) {
                response.IsSuccessful = false;
                response.message = ex.ToString();
                return StatusCode(500, response);
            }
        }

    }
}
