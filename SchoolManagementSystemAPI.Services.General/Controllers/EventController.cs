using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class EventController: ControllerBase
{
    private readonly IEventService _service;
    private readonly ResponseDTO _response;

    public EventController(IEventService service)
    {
        _service = service;
        _response = new ResponseDTO();
    }
    
    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetAllEvent()
    {
        try
        {
            _response.Result = await _service.GetEvent();
            return Ok(_response);
        }
        catch (Exception ex) { 
            _response.IsSuccessful = false;
            _response.message = ex.Message;
            return StatusCode(500, _response); }
    }
    
    [HttpPost]
    public async Task<ActionResult<ResponseDTO>> CreateEvent([FromBody] EventReqDTO eventReq)
    {
        try
        {
            await _service.CreateEvent(eventReq);
            _response.Result = "Create Event Successfully";
            return Ok(_response);
        }
        catch (Exception ex) {
            _response.IsSuccessful = false;
            _response.message = ex.ToString();    
            return StatusCode(500, _response);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDTO>> GetEventById(string id)
    {
        try
        {
            _response.Result = await _service.GetEventById(id);
            return  Ok(_response);
        }
        catch (Exception ex) {
            _response.IsSuccessful = false;
            _response.message = ex.Message;
            return StatusCode(500, _response);
        }
    }

    [HttpDelete("id")]
    public async Task<ActionResult<ResponseDTO>> DeleteEventById(string id)
    {
        try
        {
            _response.Result = await _service.DeleteEvent(id);
            return Ok(_response);
        }
        catch (Exception ex) {
            _response.IsSuccessful = false;
            _response.message = ex.Message;
            return StatusCode(500, _response);
        }
    }
   
}