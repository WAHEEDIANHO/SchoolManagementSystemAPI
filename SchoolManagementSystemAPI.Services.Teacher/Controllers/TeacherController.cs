using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Teacher.Model.DTOs;
using SchoolManagementSystemAPI.Services.Teacher.Services.IServices;
using SchoolManagementSystemAPI.Services.Teacher.Utils.GrpcService;
using SchoolManagementSystemAPI.Services.TeacherAPI;

namespace SchoolManagementSystemAPI.Services.Teacher.Controllers
{
    [ApiController]
    [Route("api/teacher")]
    public class TeacherController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly ITeacherService _service;
        private readonly IGrpcApplicationUserClientService _grpcService;

        public TeacherController(ITeacherService service, IGrpcApplicationUserClientService grpcService)
        {
            response = new ResponseDTO();
            _service = service;
            _grpcService = grpcService;
        }

        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetTeacher()
        {
            try
            {
                response.Result =  await _service.GetAllTeacher();
                /*GetTeacherRequest req = new GetTeacherRequest { Role = "TEACHER" };
                _grpcService.GetTeachers();*/
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
                response.Result = await _service.GetTeacherById(id);
                return Ok(response.Result);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(string id)
        {
            try
            {
                if (await _service.DeleteTeacherById(id))
                {
                    response.Result = "Deleted successfully";
                    return Ok(response);
                }

                response.IsSuccessful = false;
                response.message = "Unable to delete teacher";
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.ToString();
                return StatusCode(500, response);
            }
        }
    }
}
