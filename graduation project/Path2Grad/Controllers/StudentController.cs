using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Models;
using Path2Grad.Dtos;
using System.Threading.Tasks;
using System.Security.Claims;
using Path2Grad.Helper;
using Azure.Core;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
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

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var student = await GetCurrentStudentAsync();
            return Ok(student);
        }

        [HttpPost("StudentRequest")]
        public async Task<IActionResult> SendProjectRequest(RequestDto requestDto)
        {
            var student = await GetCurrentStudentAsync();
            StudentProjectJoinRequest studentProjectJoinRequest = new StudentProjectJoinRequest()
            {
                SenderId = student.StudentId,
                StudentId = requestDto.ReceiverId,
                ProjectId = student.ProjectId
            };
            _context.Add(studentProjectJoinRequest);
            await _context.SaveChangesAsync();
            return Ok("Request Sent..");
        }

        [HttpGet("StudentRequest")]
        public async Task<IActionResult> GetProjectRequest()
        {
            var student = await GetCurrentStudentAsync();
            if (student.ProjectId != null)
            {
                return Ok("You are joined in a project..");
            }
            var allRequests = await _context.StudentProjectJoinRequests
                .Where(e => e.StudentId == student.StudentId)
                .Select(e => new
                {
                    Requestid = e.RequestId,
                    SenderPic = e.Sender.Pic,
                    SenderName = e.Sender.StudentName,
                    ProjectName = e.Project.ProjectName
                })
                .ToListAsync();

            return Ok(allRequests);
        }

        [HttpPost("StatusRequest")]
        public async Task<IActionResult> StatusRequest(StatusRequestDto statusRequestDto)
        {
            var student = await GetCurrentStudentAsync();
            var request = await _context.StudentProjectJoinRequests.FirstOrDefaultAsync(e => e.RequestId == statusRequestDto.RequestId);
            if (request == null)
                return NotFound("Request not found.");

            if (statusRequestDto.Status == "Remove")
            {
                _context.StudentProjectJoinRequests.Remove(request);
            }
            else if (statusRequestDto.Status == "Accept")
            {
                student.ProjectId = request.ProjectId;
            }
            else
            {
                return NotFound("Status not correct");
            }
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpGet("Project")]
        public async Task<IActionResult> GetProject()
        {
            var student = await GetCurrentStudentAsync();

            var project = await _context.Projects
                .Include(p => p.Requirements)
                .Include(p => p.Tasks)
                .Include(p => p.projectFiles)
                .Where(p => p.ProjectId == student.ProjectId)
                .Select(p => new
                {
                    ProjectRequirements = p.Requirements.Select(x => new
                    {
                        RequirementName = x.RequirementName,
                        Pdf = x.PdfContent
                    }).ToList(),

                    ProjectFiles = p.projectFiles.Select(f => new
                    {
                        FileName = f.FileName,
                        FileContent = f.FileContent
                    }).ToList(),

                    ProjectTasks = p.Tasks.Select(t => new
                    {
                        TaskName = t.TaskName,
                        AssignedToName = t.Student.StudentName,
                        AssignedToPic = t.Student.Pic,
                        Deadline = t.Deadline,
                        Status = t.Status
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (project == null)
                return NotFound("Project not found.");

            return Ok(project);
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(AddTaskDto addTaskDto)
        {
            var student = await GetCurrentStudentAsync();
            ProjectTask projecttask = new ProjectTask()
            {
                TaskName = addTaskDto.TaskName,
                StudentId = addTaskDto.StudentId,
                ProjectId = addTaskDto.ProjectId,
                Deadline = addTaskDto.Deadline
            };
            _context.Add(projecttask);
            await _context.SaveChangesAsync();
            return Ok(projecttask);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> PostProjectfiles(ProjectFileDto file)
        {
            var student = await GetCurrentStudentAsync();
            var PDF = IFormToByteHelper.ConvertToBytes(file.File);
            ProjectFile projectFile = new ProjectFile()
            {
                FileContent = PDF,
                ProjectId = file.ProjectId
            };
            _context.Add(projectFile);
            await _context.SaveChangesAsync();
            return Ok(file.File);
        }

        [HttpPost("SupervisorRequest")]
        public async Task<IActionResult> SendProjectRequests(RequestDto requestDto)
        {
            var student = await GetCurrentStudentAsync();
            SupervisorProjectJoinRequest supervisorProjectJoinRequest = new SupervisorProjectJoinRequest()
            {
                StudentId = student.StudentId,
                SupervisorId = requestDto.ReceiverId,
                ProjectId = student.ProjectId
            };
            _context.Add(supervisorProjectJoinRequest);
            await _context.SaveChangesAsync();
            return Ok("Request Sent..");
        }
    }
}