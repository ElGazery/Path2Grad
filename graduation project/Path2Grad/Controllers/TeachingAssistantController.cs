using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Dtos;
using Path2Grad.Helper;
using Path2Grad.Models;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeachingAssistantController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeachingAssistantController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized("User is not authenticated");

            var assistant = await _context.Supervisors
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.SupervisorEmail == email);

            if (assistant == null)
                return NotFound("Assistant not found");

            return Ok(assistant);
        }

        [HttpGet("Projects")]
        public async Task<IActionResult> GetProjects()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized("User is not authenticated");

            var assistant = await _context.Supervisors
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.SupervisorEmail == email);

            if (assistant == null)
                return NotFound("Teaching Assistant not found");

            var projectIds = await _context.SupervisorProjects
                .AsNoTracking()
                .Where(sp => sp.SupervisorId == assistant.SupervisorId)
                .Select(sp => sp.ProjectId)
                .ToListAsync();

            var projects = await _context.Projects
                .AsNoTracking()
                .Where(p => projectIds.Contains(p.ProjectId))
                .Include(p => p.Students)
                    .ThenInclude(s => s.Supervisors)
                .Select(p => new
                {
                    p.ProjectId,
                    p.ProjectName,
                    p.Description,
                    Students = p.Students.Select(s => new StudentDto
                    {
                        StudentId = s.StudentId,
                        StudentName = s.StudentName,
                        Pic = s.Pic
                    }).ToList(),
                    Doctors = _context.SupervisorProjects
                        .Where(sp => sp.ProjectId == p.ProjectId && sp.Supervisor.Position == "Doctor")
                        .Select(sp => new
                        {
                            DoctorId = sp.Supervisor.SupervisorId,
                            DoctorName = sp.Supervisor.SupervisorName,
                            DoctorPic = sp.Supervisor.Pic
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
                    p.ProjectFields,
                    p.Description,
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

        [HttpPost("Requirement")]
        public async Task<IActionResult> PostRequirement([FromForm] ProjectRequirementCreateDto dto)
        {
            var projectRequirement = ProjectRequirementHelper.ToEntity(dto);

            await _context.ProjectRequirements.AddAsync(projectRequirement);
            await _context.SaveChangesAsync();

            return Ok(projectRequirement);
        }

        [HttpGet("ProjectRequest")]
        public async Task<IActionResult> GetProjectRequest()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _context.Supervisors.FirstOrDefaultAsync(s => s.SupervisorEmail == email);

            var allRequests = await _context.SupervisorProjectJoinRequests
                .Where(e => e.SupervisorId == doctor.SupervisorId)
                .Include(e => e.Project)
                .Include(e => e.Supervisor)
                .ToListAsync();

            var result = new List<object>();

            foreach (var request in allRequests)
            {
                var studentName = await _context.Students
                    .Where(s => s.StudentId == request.StudentId)
                    .Select(s => s.StudentName)
                    .FirstOrDefaultAsync();

                result.Add(new
                {
                    RequestId = request.RequestId,
                    SenderPic = request.Supervisor?.Pic,
                    SenderName = studentName,
                    ProjectName = request.Project?.ProjectName,
                    projectId=request.ProjectId
                });
            }

            return Ok(result);
        }
        [HttpPost("StatusRequest")]
        public async Task<IActionResult> StatusRequest(SupervisorStatusRequestDto RequestDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var doctor = await _context.Supervisors.FirstOrDefaultAsync(s => s.SupervisorEmail == email);

            var project = await _context.SupervisorProjects.FirstOrDefaultAsync(s=>s.ProjectId==RequestDto.ProjectId);
            var request = await _context.SupervisorProjectJoinRequests.FirstOrDefaultAsync(e => e.RequestId == RequestDto.RequestId);
            
            if (request == null)
                return NotFound("Request not found.");

            if (RequestDto.Status == "Remove")
            {
                _context.SupervisorProjectJoinRequests.Remove(request);
            }
            else if (RequestDto.Status == "Accept")
            {
                var supervisorproject = new SupervisorProject()
                {
                    ProjectId = project.ProjectId,
                    SupervisorId = doctor.SupervisorId
                };
                await _context.AddAsync(supervisorproject);
                 _context.Remove(request);

            }
            else
            {
                return NotFound("Status not correct");
            }

            await _context.SaveChangesAsync();
            return Ok("The project was successfully accepted ");
        }

    }
}
