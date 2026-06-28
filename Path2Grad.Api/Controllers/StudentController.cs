using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Student")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        private async Task<int?> GetCurrentStudentIdAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            if (email == null) return null;

            var student = await _studentService.GetProfileAsync(email);
            return student?.StudentId;
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> GetProfile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            return Ok(student);
        }

        [HttpPost("StudentRequest")]
        public async Task<IActionResult> SendProjectRequest(RequestDto requestDto)
        {
            var studentId = await GetCurrentStudentIdAsync();
            if (studentId == null) return Unauthorized();

            await _studentService.SendProjectRequestAsync(studentId.Value, requestDto.ReceiverId, null);
            return Ok("Request Sent..");
        }

        [HttpGet("StudentRequest")]
        public async Task<IActionResult> GetProjectRequest()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            if (student == null) return Unauthorized();

            var result = await _studentService.GetProjectRequestsAsync(student.StudentId, student.ProjectId);
            return Ok(result);
        }

        [HttpPost("StatusRequest")]
        public async Task<IActionResult> StatusRequest(StatusRequestDto statusRequestDto)
        {
            var studentId = await GetCurrentStudentIdAsync();
            if (studentId == null) return Unauthorized();

            var result = await _studentService.HandleStatusRequestAsync(studentId.Value, statusRequestDto.RequestId, statusRequestDto.Status);
            if (result == null)
                return NotFound("Request not found or status not correct");

            return Ok(result);
        }

        [HttpGet("Project")]
        public async Task<IActionResult> GetProject()
        {
            var studentId = await GetCurrentStudentIdAsync();
            if (studentId == null) return Unauthorized();

            var project = await _studentService.GetProjectAsync(studentId.Value);
            if (project == null)
                return NotFound("Project not found.");

            return Ok(project);
        }

        [HttpPost("AddTask")]
        public async Task<IActionResult> AddTask(AddTaskDto addTaskDto)
        {
            await _studentService.AddTaskAsync(addTaskDto);
            return Ok(addTaskDto);
        }

        [HttpDelete("Task/{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            await _studentService.DeleteTaskAsync(taskId);
            return Ok("Task deleted successfully");
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> PostProjectfiles([FromForm] ProjectFileDto file)
        {
            var studentId = await GetCurrentStudentIdAsync();
            if (studentId == null) return Unauthorized();

            await _studentService.UploadFileAsync(studentId.Value, file);
            return Ok(file.File);
        }

        [HttpPost("SupervisorRequest")]
        public async Task<IActionResult> SendProjectRequests(RequestDto requestDto)
        {
            var studentId = await GetCurrentStudentIdAsync();
            if (studentId == null) return Unauthorized();

            await _studentService.SendSupervisorRequestAsync(studentId.Value, requestDto.ReceiverId, null);
            return Ok("Request Sent..");
        }
    }
}
