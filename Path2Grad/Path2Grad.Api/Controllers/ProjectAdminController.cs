using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectAdminController : ControllerBase
    {
        private readonly IProjectAdminService _adminService;

        public ProjectAdminController(IProjectAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("Data")]
        public async Task<IActionResult> Get()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("User is not authenticated");

            var admin = await _adminService.GetProfileAsync(email);
            return Ok(admin);
        }

        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _adminService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("ProjectById/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _adminService.GetProjectByIdAsync(id);
            if (project == null)
                return NotFound("Project not found");
            return Ok(project);
        }

        [HttpDelete("Project/{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            await _adminService.DeleteProjectAsync(id);
            return Ok("Project deleted successfully");
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var admin = await _adminService.GetProfileAsync(email);
            return Ok(admin);
        }
    }
}
