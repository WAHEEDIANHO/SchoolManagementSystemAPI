using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class NoteController: ControllerBase
{
    private readonly INoteService _service;
    private readonly ResponseDTO response;

    public NoteController(INoteService service)
    {
        _service = service;
        response = new ResponseDTO();
    }
    
    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetNotes([FromQuery] Dictionary<string, string>? query)
    {
        try
        {
            if (query == null || !query.Any()) response.Result = await _service.GetAll();
            else response.Result = await _service.GetAll(query);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseDTO>> GetNoteById(string id)
    {
        try
        {
            response.Result = await _service.GetById(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    [HttpPost]
    public async Task<ActionResult<ResponseDTO>> AddNote([FromBody] NoteDto note)
    {
        try
        {
            response.Result = await _service.Add(note);
            return Created(nameof(NoteController), response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseDTO>> DeleteNote(string id)
    {
        try
        {
            response.Result = await _service.Delete(id);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetStudentPersonalNotes([FromQuery] string studentId, [FromQuery] string lessonId)
    {
        try
        {
            response.Result = await _service.GetStudentPersonalNotes(studentId, lessonId);
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