using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Dtos;
using Path2Grad.Models;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectAdminController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Data")]
        public async Task<IActionResult> Get()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (string.IsNullOrEmpty(email))
                return Unauthorized("User is not authenticated");

            var admin = await _context.ProjectsAdmins
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.AdminEmail == email);

            return Ok(admin);
        }

        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects
                .AsNoTracking()
                .Include(p => p.Students)
                    .ThenInclude(s => s.Supervisors)
                .Select(p => new
                {
                    ProjectId = p.ProjectId,
                    ProjectName = p.ProjectName,
                    Description = p.Description,
                    Students = p.Students.Select(s => new StudentDto
                    {
                        StudentId = s.StudentId,
                        StudentName = s.StudentName,
                        Pic = s.Pic
                    }).ToList(),
                    Supervisors = _context.SupervisorProjects
                        .Where(sp => sp.ProjectId == p.ProjectId)
                        .Select(sp => new SupervisorDto
                        {
                            SupervisorId = sp.Supervisor.SupervisorId,
                            SupervisorName = sp.Supervisor.SupervisorName,
                            Pic = sp.Supervisor.Pic
                        }).ToList()
                })
                .ToListAsync();

            return Ok(projects);
        }

        [HttpGet("ProjectById/{id}")]
        public async Task<IActionResult> GetProject(int id)
        {
            var project = await _context.Projects
                .AsNoTracking()
                .Include(p => p.Students)
                    .ThenInclude(s => s.Supervisors)
                .Where(p => p.ProjectId == id)
                .Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    p.Description,
                    p.ProjectFields,
                    p.NumberOfTeam,
                    Students = p.Students.Select(s => new StudentDto
                    {
                        StudentId = s.StudentId,
                        StudentName = s.StudentName,
                        Pic = s.Pic
                    }).ToList(),
                    Supervisors = _context.SupervisorProjects
                        .Where(sp => sp.ProjectId == p.ProjectId)
                        .Select(sp => new SupervisorDto
                        {
                            SupervisorId = sp.Supervisor.SupervisorId,
                            SupervisorName = sp.Supervisor.SupervisorName,
                            Pic = sp.Supervisor.Pic
                        }).ToList()
                })
                .FirstOrDefaultAsync();

            if (project == null)
                return NotFound("Project not found");

            return Ok(project);
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var admin = await _context.ProjectsAdmins
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.AdminEmail == email);

            return Ok(admin);
        }
    }
}
