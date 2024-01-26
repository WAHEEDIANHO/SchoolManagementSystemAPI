using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Teacher.Controllers
{
    [Route("api/teacher")]
    public class TeacherController : Controller
    {
        private readonly ResponseDTO response;
        private readonly ITeacherService _service;

        public TeacherController(ITeacherService service)
        {
            response = new ResponseDTO();
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetTeacher()
        {
            try
            {
                response.Result =  await _service.GetAllTeacher();
                return Ok(response);
            }catch (Exception ex) {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);

            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetTeacher(string id)
        {
            try
            {
                response.Result = await _service.GetStudentById(id);
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }
    }
}
