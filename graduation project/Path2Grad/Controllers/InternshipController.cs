using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Helper;
using Path2Grad.Models;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class InternshipController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InternshipController(ApplicationDbContext context)
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

        [HttpGet("Internship")]
        public async Task<IActionResult> GetAllInternship()
        {
            var intern = await _context.Internships
                .Select(e => new
                {
                    Name = e.InternshipName,
                    Link = e.InternshipLink
                })
                .ToListAsync();

            return Ok(intern);
        }

        [HttpPost("WorkFiles")]
        public async Task<IActionResult> PostWorkFile(IFormFile file)
        {
            var student = await GetCurrentStudentAsync();
            if (student == null)
                return Unauthorized("Student not found.");

            var pdf = IFormToByteHelper.ConvertToBytes(file);
            var fileEntity = new InternshipWorkFile
            {
                WorkFile = pdf,
                StudentId = student.StudentId
            };

            await _context.InternshipWorkFiles.AddAsync(fileEntity);
            await _context.SaveChangesAsync();

            return Ok(fileEntity);
        }

        [HttpPost("Certificates")]
        public async Task<IActionResult> PostCertificate(IFormFile file)
        {
            var student = await GetCurrentStudentAsync();
            if (student == null)
                return Unauthorized("Student not found.");

            var pdf = IFormToByteHelper.ConvertToBytes(file);
            var certEntity = new InternshipCertificate
            {
                Certificate = pdf,
                StudentId = student.StudentId
            };

            await _context.InternshipCertificates.AddAsync(certEntity);
            await _context.SaveChangesAsync();

            return Ok(certEntity);
        }

        [HttpGet("WorkFiles")]
        public async Task<IActionResult> GetWorkFile()
        {
            var student = await GetCurrentStudentAsync();
            if (student == null)
                return Unauthorized("Student not found.");

            var files = await _context.InternshipWorkFiles
                .Where(e => e.StudentId == student.StudentId)
                .Select(e => new
                {
                    e.WorkFile,
                    e.InternshipWorkFilesId
                })
                .ToListAsync();

            return Ok(files);
        }

        [HttpGet("UploadCertificates")]
        public async Task<IActionResult> GetCertificate()
        {
            var student = await GetCurrentStudentAsync();
            if (student == null)
                return Unauthorized("Student not found.");

            var certificates = await _context.InternshipCertificates
                .Where(e => e.StudentId == student.StudentId)
                .Select(e => new
                {
                    e.InternshipCertificatesId,
                    e.Certificate
                })
                .ToListAsync();

            return Ok(certificates);
        }
    }
}
