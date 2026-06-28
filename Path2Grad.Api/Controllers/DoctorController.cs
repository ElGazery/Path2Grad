using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var doctor = await _doctorService.GetProfileAsync(email);
            return Ok(doctor);
        }

        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var projects = await _doctorService.GetProjectsAsync(email);
            return Ok(projects);
        }

        [HttpGet("ProjectById/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _doctorService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound("Project not found");
            return Ok(project);
        }

        [HttpPost("Requirement")]
        public async Task<IActionResult> PostRequirement([FromForm] ProjectRequirementCreateDto dto)
        {
            await _doctorService.AddRequirementAsync(dto);
            return Ok(dto);
        }

        [HttpGet("ProjectFiles")]
        public async Task<IActionResult> GetProjectFiles()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var files = await _doctorService.GetProjectFilesAsync(email);
            return Ok(files);
        }

        [HttpGet("ProjectRequest")]
        public async Task<IActionResult> GetProjectRequest()
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var requests = await _doctorService.GetProjectRequestsAsync(email);
            return Ok(requests);
        }

        [HttpDelete("Requirement/{id}")]
        public async Task<IActionResult> DeleteRequirement(int id)
        {
            await _doctorService.DeleteRequirementAsync(id);
            return Ok("Requirement deleted successfully");
        }

        [HttpPost("StatusRequest")]
        public async Task<IActionResult> StatusRequest(SupervisorStatusRequestDto requestDto)
        {
            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var result = await _doctorService.HandleStatusRequestAsync(email, requestDto);
            if (result == "Doctor not found")
                return NotFound("Doctor not found");
            if (result == "Request not found.")
                return NotFound("Request not found.");
            return Ok(result);
        }
    }
}
