using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Helper;
using Path2Grad.Models;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class CVController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CVController(ApplicationDbContext context)
        {
            _context = context;
        }

        private async Task<Student?> GetCurrentStudentAsync()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (email == null)
                return null;

            return await _context.Students.FirstOrDefaultAsync(d => d.StudentEmail == email);
        }

        [HttpGet("CVTemplets")]
        public async Task<IActionResult> GetAllCVTemplets()
        {
            var CVs = await _context.Cvs.Where(e => e.Type == "Templet").ToListAsync();
            return Ok(CVs);
        }

        [HttpPost("AddStudentCV")]
        public async Task<IActionResult> PostCv(IFormFile cv)
        {
            var student = await GetCurrentStudentAsync();
            if (student == null)
                return Unauthorized("Student not found.");

            var pdf =  IFormToByteHelper.ConvertToBytes(cv);
            var studentCv = new Cv
            {
                Cvfile = pdf,
                StudentId = student.StudentId,
                Type = "StudentCV",
                CVName = cv.FileName
            };

            _context.Cvs.Add(studentCv);
            await _context.SaveChangesAsync();

            return Ok(studentCv);
        }
    }
}
