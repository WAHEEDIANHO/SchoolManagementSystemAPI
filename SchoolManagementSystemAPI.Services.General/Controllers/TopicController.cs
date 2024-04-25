using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.General.Model.Dto;
using SchoolManagementSystemAPI.Services.General.Model.DTOs;
using SchoolManagementSystemAPI.Services.General.Repositories.Schema;
using SchoolManagementSystemAPI.Services.General.Services.IService;

namespace SchoolManagementSystemAPI.Services.General.Controllers;

[Route("topic")]
[ApiController]
public class TopicController: ControllerBase
{
    private readonly ITopicService _service;
    private readonly ResponseDTO response;
    public TopicController(ITopicService service)
    {
        _service = service;
        response = new ResponseDTO();
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDTO>> AddTopic([FromBody] TopicReqDTO topicReq)
    {
        try
        {
            response.Result = await _service.AddTopic(topicReq);
            return Created(nameof(TopicController),  response);
        }
        catch (Exception ex)
        {
            response.message = ex.ToString();
            response.IsSuccessful = false;
            return StatusCode(500, response);
        }
    }

    [HttpGet("subject-topics")]
    public async Task<ActionResult<ResponseDTO>> GetSubjectTopics([FromQuery] int GradeNumber,
        [FromQuery] string SubjectTitle)
    {
        try
        {
           response.Result = await _service.GetGradeSubjectTopics(GradeNumber, SubjectTitle);
           return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpGet("{topicId}")]
    public async Task<ActionResult<ResponseDTO>> GetTopicById(string topicId)
    {
        try
        {
            var topic = await _service.GetTopicById(topicId);
            response.Result = topic;
            return Ok(response);
        }
        catch (Exception ex)
        {
            response.IsSuccessful = false;
            response.message = ex.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpPut]
    public async Task<ActionResult<ResponseDTO>> UpdateTopic([FromBody] TopicDTO upTopic)
    {
        try
        {
            response.Result = await _service.UpdateTopic(upTopic);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpDelete("{topicId}")]
    public async Task<ActionResult<ResponseDTO>> DeleteTopic(string topicId)
    {
        try
        {
            response.Result = await _service.DeleteTopic(topicId);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    /*******************************************
     * Lesson Session *
     *******************************************/

    [HttpGet("lesson")]
    public async Task<ActionResult<ResponseDTO>> GetAllTopicLessons([FromQuery] string topicId)
    {
        try
        {
            response.Result = await _service.GetTopicLesson(topicId);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    [HttpGet("lesson/{lessonId}")]
    public async Task<ActionResult<ResponseDTO>> GetLessonById(string lessonId)
    {
        try
        {
            response.Result = await _service.GetLessonById(lessonId);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpPost("lesson")]
    public async Task<ActionResult<ResponseDTO>> AddLessonToTopic([FromForm] LessonReqDTO lessonReq)
    {
        try
        {
            response.Result = await _service.AddLesson(lessonReq);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }

    [HttpPut("lesson")]
    public async Task<ActionResult<ResponseDTO>> UpdateLesson([FromForm] LessonDTO lesson)
    {
        try
        {
            response.Result = await _service.UpdateLesson(lesson);
            return Ok(response);
        }
        catch (Exception e)
        {
            response.IsSuccessful = false;
            response.message = e.ToString();
            return StatusCode(500, response);
        }
    }
    
    [HttpDelete("lesson/{lessonId}")]
    public async Task<ActionResult<ResponseDTO>> DeleteLesson(string lessonId)
    {
        try
        {
            response.Result = await _service.DeleteLesson(lessonId);
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