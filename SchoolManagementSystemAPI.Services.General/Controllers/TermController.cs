using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class TermController: ControllerBase
{
    private readonly ITermService _service;
    private readonly ResponseDTO response;

    public TermController(ITermService service)
    {
        _service = service;
        response = new ResponseDTO();
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetTerms([FromQuery] Dictionary<string, string>? query)
    {
        try
        {
            if (query == null || !query.Any())
                response.Result = await _service.GetAll();
            else response.Result = await _service.GetAll(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.message = ex.Message;
            return StatusCode(500, response);
        }
    }

    [HttpGet("{termId}")]
    public async Task<ActionResult<ResponseDTO>> GetTermById(string termId)
    {
        try
        {
            response.Result = await _service.GetById(termId);
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.message = ex.Message;
            return StatusCode(500, response);  
        }
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDTO>> AddTerm(TermDto term)
    {
        try
        {
            if (await _service.Add(term))
                response.Result = "Term added successfully";
            else
                response.Result = "Term added successfully";
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);  
        }
    }

    [HttpDelete]
    public async Task<ActionResult<ResponseDTO>> DeleteTerm([FromQuery]string sessionName, [FromQuery]int termNumber)
    {
        try
        {
            if(await _service.Delete(sessionName, termNumber))
                response.Result = "Term remove successfully";
            else
                response.Result = "Unable to remove term ";
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.Message;
            return StatusCode(500, response);   
        }
    }

    [HttpGet]
    public async Task<ActionResult<ResponseDTO>> GetCurrentTerm()
    {
        try
        {
            response.Result = await _service.GetCurrentTerm();
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.Message;
            return StatusCode(500, response);   
        }
    }
}