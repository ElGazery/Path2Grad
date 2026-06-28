using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Path2Grad.Dtos;
using Path2Grad.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class TrackController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrackController(ApplicationDbContext context)
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

        [HttpGet("GetAllTracks")]
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = _context.Tracks.Select(e=> new { e.TrackId,e.TrackName}).ToList();
            return Ok(tracks);
        }

        [HttpPost("AddTrack")]
        public async Task<IActionResult> PostTrack(AddTrackDto addTrackDto)
        {
            var student = await GetCurrentStudentAsync();

            var track = await _context.Tracks.FirstOrDefaultAsync(e => e.TrackName == addTrackDto.TrackName);
            if (track == null)
                return NotFound("Track not found");

            student.TrackId = track.TrackId;
            _context.Update(student);
            await _context.SaveChangesAsync();
            return Ok(student);
        }

        [HttpGet("Track")]
        public async Task<IActionResult> GetTrack()
        {
            var student = await GetCurrentStudentAsync();
            if (student.TrackId is null)
            {
                return BadRequest("Enroll in a track");
            }

            var tracks = await _context.Tracks
                         .Where(e => e.TrackId == student.TrackId)
                         .Include(t => t.Items)
                         .ThenInclude(i => i.ItemLessons)
                         .Select(t => new
                         {
                             TrackName = t.TrackName,
                             Items = t.Items.Select(i => new
                             {
                                 ItemName = i.Name,
                                 Lessons = i.ItemLessons.Select(l => new
                                 {
                                     LessonId=l.Id,
                                     LessonName = l.Name,
                                     IsComplet = l.IsComplet
                                 }).ToList()
                             }).ToList()
                         })
                         .ToListAsync();

            return Ok(tracks);
        }

        [HttpGet("TrackRate")]
        public async Task<IActionResult> GetRate()
        {
            var student = await GetCurrentStudentAsync();

            var totalLessons = await _context.TrackItem
                             .Where(e => e.TrackId == student.TrackId)
                             .SelectMany(e => e.ItemLessons)
                             .CountAsync();

            var completedLessons = await _context.TrackItem
                .Where(e => e.TrackId == student.TrackId)
                .SelectMany(e => e.ItemLessons)
                .CountAsync(lesson => lesson.IsComplet == true);

            double percentComplete = totalLessons > 0 ? (double)completedLessons / totalLessons * 100 : 0;

            return Ok(new { Message = "Track Rate", PercentComplete = percentComplete });
        }
        [HttpPut("UpdateTrack/{id}")]
        public async Task<IActionResult> UpdateTrack(int id)
        {
           var student = await GetCurrentStudentAsync();
            student.TrackId = id;
            await _context.SaveChangesAsync();
            return Ok("track updated successfully ");

        }

        [HttpPut("UpdateLessonStatus")]
        public async Task<IActionResult> UpdateLessonStatus([FromBody] LessonUpdateDto model)
        {
            var student = await GetCurrentStudentAsync();

            var lesson = await _context.TrackItem
                .Where(e => e.TrackId == student.TrackId)
                .SelectMany(e => e.ItemLessons)
                .FirstOrDefaultAsync(l => l.Id == model.LessonId);

            if (lesson == null)
                return NotFound("Lesson not found");

            lesson.IsComplet = model.IsComplet;
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Lesson updated successfully" });
        }
    }
}