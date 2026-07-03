using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachingAssistantController : ControllerBase
    {
        private readonly ITeachingAssistantService _taService;

        public TeachingAssistantController(ITeachingAssistantService taService)
        {
            _taService = taService;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized("User is not authenticated");

            var assistant = await _taService.GetProfileAsync(email);
            if (assistant == null)
                return NotFound("Assistant not found");

            return Ok(assistant);
        }

        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized("User is not authenticated");

            var projects = await _taService.GetProjectsAsync(email);
            return Ok(projects);
        }

        [HttpGet("ProjectById/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _taService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound("Project not found");
            return Ok(project);
        }

        [HttpPost("Requirement")]
        public async Task<IActionResult> PostRequirement([FromForm] ProjectRequirementCreateDto dto)
        {
            await _taService.AddRequirementAsync(dto);
            return Ok(dto);
        }

        [HttpGet("ProjectRequest")]
        public async Task<IActionResult> GetProjectRequest()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var requests = await _taService.GetProjectRequestsAsync(email);
            return Ok(requests);
        }

        [HttpDelete("Requirement/{id}")]
        public async Task<IActionResult> DeleteRequirement(int id)
        {
            await _taService.DeleteRequirementAsync(id);
            return Ok("Requirement deleted successfully");
        }

        [HttpPost("StatusRequest")]
        public async Task<IActionResult> StatusRequest(SupervisorStatusRequestDto requestDto)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var result = await _taService.HandleStatusRequestAsync(email, requestDto);
            return Ok(result);
        }
    }
}
