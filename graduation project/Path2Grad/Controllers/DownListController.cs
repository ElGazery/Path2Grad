using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownListController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DownListController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("ProjectField")]
        public async Task<IActionResult> GetProjectField()
        {
            var fields = await _context.Fields.Distinct().ToListAsync();
            return Ok(fields);
        }

        [HttpGet("Supervisor")]
        public async Task<IActionResult> GetSupervisor()
        {
            var supervisors = await _context.Supervisors
                .Where(e => e.Position == "Doctor")
                .Select(s => new
                {
                    s.SupervisorId,
                    s.SupervisorName,
                    s.Pic,
                    s.Specialization
                })
                .ToListAsync();

            return Ok(supervisors);
        }

        [HttpGet("CoSupervisor")]
        public async Task<IActionResult> GetCoSupervisor()
        {
            var supervisors = await _context.Supervisors
                .Where(e => e.Position == "TeachingAssistant")
                .Select(s => new
                {
                    s.SupervisorId,
                    s.SupervisorName,
                    s.Pic,
                    s.Specialization
                })
                .ToListAsync();

            return Ok(supervisors);
        }

        [HttpGet("Student/{trackName}")]
        public async Task<IActionResult> GetStudentByTrack(string trackName)
        {
            var students = await _context.Students
                .Where(s => s.Track.TrackName == trackName)
                .Select(e => new
                {
                    e.StudentId,
                    e.StudentName,
                    track = e.Track.TrackName,
                    e.Pic
                })
                .ToListAsync();

            return Ok(students);
        }
    }
}
