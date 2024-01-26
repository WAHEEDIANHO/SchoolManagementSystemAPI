using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystemAPI.Services.Parent.Model.DTOs;
using SchoolManagementSystemAPI.Services.Parent.Services.IServices;

namespace SchoolManagementSystemAPI.Services.Parent.Controllers
{
    [Route("api/parent")]
    public class ParentController : Controller
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
    }
}
