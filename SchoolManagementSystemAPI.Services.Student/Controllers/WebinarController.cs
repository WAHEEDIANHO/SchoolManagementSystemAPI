using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Student.Model.DTOs;
using SchoolManagementSystemAPI.Services.Student.Utils;
using SchoolManagementSystemAPI.Services.Student.Utils.GrpcService.IGrpcClientService;

namespace SchoolManagementSystemAPI.Services.Student.Controllers;
[ApiController]
[Route("api/student/webinar")]
public class WebinarController : ControllerBase
{
    private readonly ResponseDTO response;
    private readonly IGrpcWebinarClientService _service;

    public WebinarController(IGrpcWebinarClientService service)
    {
        response = new ResponseDTO();
        _service = service;
    }

    [Authorize(Roles = UserRoles.STUDENT)]
    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetUpComingWebinars([FromQuery] int GradeNumber)
    {
        try
        {
            response.Result = await _service.GetUpcomingWebinars(GradeNumber);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    [Authorize(Roles = UserRoles.STUDENT)]
    [HttpGet("today")]
    public async Task<ActionResult<ResponseDTO>> GetTodayWebinar([FromQuery] int GradeNumber)
    {
        try
        {
            response.Result = await _service.GetCurrentWebinar(GradeNumber);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
}




