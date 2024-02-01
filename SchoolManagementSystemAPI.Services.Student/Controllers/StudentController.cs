using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Student.Model.DTO;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Student.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _service;
        private readonly ResponseDTO response;

        public StudentController(IStudentService service)
        {
            _service = service;
            response = new ResponseDTO();

        }

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

    }
}
