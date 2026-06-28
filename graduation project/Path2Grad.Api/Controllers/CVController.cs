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
    public class CVController : ControllerBase
    {
        private readonly ICVService _cvService;
        private readonly IStudentService _studentService;

        public CVController(ICVService cvService, IStudentService studentService)
        {
            _cvService = cvService;
            _studentService = studentService;
        }

        [HttpGet("CVTemplets")]
        public async Task<IActionResult> GetAllCVTemplets()
        {
            var cvs = await _cvService.GetTemplatesAsync();
            return Ok(cvs);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCv(int id)
        {
            await _cvService.DeleteCvAsync(id);
            return Ok("CV deleted successfully");
        }

        [HttpPost("AddStudentCV")]
        public async Task<IActionResult> PostCv(IFormFile cv)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var student = await _studentService.GetProfileAsync(email);
            if (student == null)
                return Unauthorized("Student not found.");

            var pdf = IFormToByteHelper.ConvertToBytes(cv);
            var studentCv = await _cvService.AddStudentCvAsync(student.StudentId, pdf, cv.FileName);
            return Ok(studentCv);
        }
    }
}
