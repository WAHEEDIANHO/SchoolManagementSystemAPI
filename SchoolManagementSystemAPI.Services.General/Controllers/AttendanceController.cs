using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _service;
        private readonly ResponseDTO response;
        private int statusCode = 200;

        public AttendanceController(IAttendanceService service) 
        {
            _service = service;
            response = new ResponseDTO();
        }

        [HttpPost()]
        public async Task<ActionResult<ResponseDTO>> CreateSheet([FromBody] CreateAttendanceSheet createAttendanceHeader)
        {
            try
            {
                response.Result = await _service.CreateGradeAttendanceSheet(createAttendanceHeader); 
                return Ok(response);
            }catch (Exception ex) {
                statusCode = 500;
                response.IsSuccessful = false;
                response.message = ex.ToString();
                return StatusCode(statusCode, response);
            }
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetAllAttendantSheet()
        {
            try
            {
               response.Result = await _service.GetAttendanceSheet();
                return Ok(response);

            }catch (Exception ex)
            {
                statusCode = 500;
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(statusCode, response);
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponseDTO>> GetStudentAttendant([FromQuery] string attendanceSheetId, string userId)
        {
            try
            {
                response.Result = await _service.GetStudentAttendant(attendanceSheetId, userId);
                return Ok(response);

            }
            catch (Exception ex)
            {
                statusCode = 500;
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(statusCode, response);
            }
        }
    }
}
