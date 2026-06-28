using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Helpers;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InternshipController : ControllerBase
    {
        private readonly IInternshipService _internshipService;
        private readonly IStudentService _studentService;

        public InternshipController(IInternshipService internshipService, IStudentService studentService)
        {
            _internshipService = internshipService;
            _studentService = studentService;
        }

        [HttpGet("Internship")]
        public async Task<IActionResult> GetAllInternship()
        {
            var internships = await _internshipService.GetAllInternshipsAsync();
            return Ok(internships);
        }

        [HttpPost("WorkFiles")]
        public async Task<IActionResult> PostWorkFile(IFormFile file)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            if (student == null)
                return Unauthorized("Student not found.");

            var pdf = IFormToByteHelper.ConvertToBytes(file);
            var result = await _internshipService.UploadWorkFileAsync(student.StudentId, pdf);
            return Ok(result);
        }

        [HttpDelete("WorkFile/{id}")]
        public async Task<IActionResult> DeleteWorkFile(int id)
        {
            await _internshipService.DeleteWorkFileAsync(id);
            return Ok("Work file deleted successfully");
        }

        [HttpDelete("Certificate/{id}")]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            await _internshipService.DeleteCertificateAsync(id);
            return Ok("Certificate deleted successfully");
        }

        [HttpPost("Certificates")]
        public async Task<IActionResult> PostCertificate(IFormFile file)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            if (student == null)
                return Unauthorized("Student not found.");

            var pdf = IFormToByteHelper.ConvertToBytes(file);
            var result = await _internshipService.UploadCertificateAsync(student.StudentId, pdf);
            return Ok(result);
        }

        [HttpGet("WorkFiles")]
        public async Task<IActionResult> GetWorkFile()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            if (student == null)
                return Unauthorized("Student not found.");

            var files = await _internshipService.GetWorkFilesAsync(student.StudentId);
            return Ok(files);
        }

        [HttpGet("UploadCertificates")]
        public async Task<IActionResult> GetCertificate()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            if (student == null)
                return Unauthorized("Student not found.");

            var certificates = await _internshipService.GetCertificatesAsync(student.StudentId);
            return Ok(certificates);
        }
    }
}
