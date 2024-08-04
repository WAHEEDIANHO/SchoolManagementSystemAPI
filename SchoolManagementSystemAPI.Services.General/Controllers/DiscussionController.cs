using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class DiscussionController: ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ResponseDTO response;
    private readonly IDiscussionService _service;

    public DiscussionController(IMapper mapper, IDiscussionService service)
    {
        response = new ResponseDTO();
        _mapper = mapper;
        _service = service;
    }
    
    
    [Authorize(Roles = "Teacher, Student")]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO>> CreateDiscussion([FromBody] DiscussionReqDto discussionReqDto)
    {
        var user = User.FindFirstValue(ClaimTypes.Name);
        try
        {
            response.Result = user;  //await _service.Add(discussionReqDto);
            return Ok(response);
        }catch (Exception ex)
        {
            // statusCode = 500;
            response.IsSuccessful = false;
            response.message = ex.Message;
            return StatusCode(500, response);
        }
    }
}