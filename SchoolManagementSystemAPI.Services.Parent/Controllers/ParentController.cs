using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Parent.Controllers
{
    [ApiController]
    [Route("api/parent")]
    public class ParentController : ControllerBase
    {
        private readonly ResponseDTO response;
        private readonly IParentService _service;
        public ParentController(IParentService service)
        {
            _service = service;
            response = new ResponseDTO();
        }
        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> GetParent()
        {
            try
            {
                response.Result = await _service.GetAllParent();
                return Ok(response);
            }catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDTO>> GetParentById(string id)
        {
            try
            {
                response.Result = await _service.GetParentById(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.message = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveStudent(string id)
        {
            try
            {
                if (await _service.DeleteParentById(id))
                {
                    response.Result = "Deleted successfully";
                    return Ok(response);
                }

                response.IsSuccessful = false;
                response.message = "Unable to delete student";
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
