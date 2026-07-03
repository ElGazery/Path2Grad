using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet("ProjectBank")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _projectService.GetProjectBankAsync();
            return Ok(projects);
        }

        [HttpGet("ProjectBank/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _projectService.GetProjectBankByIdAsync(id);
            if (project == null)
                return BadRequest("Please enter a valid ID!");
            return Ok(project);
        }

        [HttpPost("CustomizeProject")]
        public async Task<IActionResult> PostProject(ProjectDto projectDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _projectService.CustomizeProjectAsync(email, projectDto);
            if (result == "Student not found or not authenticated")
                return Unauthorized(result);
            return Ok(result);
        }
    }
}
