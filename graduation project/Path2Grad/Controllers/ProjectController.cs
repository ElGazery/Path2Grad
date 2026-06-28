using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Dtos;
using Path2Grad.Models;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<Student> GetCurrentStudentAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null)
                return null;

            return await _context.Students.FirstOrDefaultAsync(d => d.StudentEmail == email);
        }

        [HttpGet("ProjectBank")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.ProjectsBanks
                .Select(e => new
                {
                    ProjectId = e.ProjectBankId,
                    ProjectName = e.ProjectName,
                    ProjectDescripition = e.Description
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("ProjectBank/{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _context.ProjectsBanks
                .Where(e => e.ProjectBankId == id)
                .Select(e => new
                {
                    ProjectId = e.ProjectBankId,
                    ProjectName = e.ProjectName,
                    ProjectDescripition = e.Description,
                    e.ProjectSpecification,
                    projectFields = e.ProjectsBankProjectFields
                        .Where(p => p.ProjectFieldId == id)
                        .Select(p => new
                        {
                            p.ProjectFieldId,
                            p.ProjectField
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();

            if (project == null)
                return BadRequest("Please enter a valid ID!");

            return Ok(project);
        }

        [HttpPost("CustomizeProject")]
        public async Task<IActionResult> PostProject(ProjectDto projectDto)
        {
            var student = await GetCurrentStudentAsync();
            if (student == null)
                return Unauthorized("Student not found or not authenticated");

            var project = new Project
            {
                ProjectName = projectDto.ProjectName,
                Description = projectDto.Description,
                NumberOfTeam = projectDto.NumberOfTeam,
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            var fieldNames = projectDto.Fields.Select(f => f.FieldName).ToList();

            var fields = await _context.Fields
                .Where(f => fieldNames.Contains(f.FieldName))
                .ToListAsync();

            var projectFields = fields.Select(field => new ProjectField
            {
                ProjectId = project.ProjectId,
                FieldId = field.FieldId
            }).ToList();

            student.ProjectId = project.ProjectId;

            _context.ProjectFields.AddRange(projectFields);
            await _context.SaveChangesAsync();

            return Ok("Project created successfully. Start to add supervisors and students.");
        }
    }
}
