using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet("GetAllTracks")]
        public async Task<IActionResult> GetAllTracks()
        {
            var tracks = await _trackService.GetAllTracksAsync();
            return Ok(tracks);
        }

        [HttpPost("AddTrack")]
        public async Task<IActionResult> PostTrack(AddTrackDto addTrackDto)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _trackService.AddTrackToStudentAsync(email, addTrackDto.TrackName);
            if (result == "Student not found")
                return NotFound("Student not found");
            if (result == "Track not found")
                return NotFound("Track not found");
            return Ok(result);
        }

        [HttpGet("Track")]
        public async Task<IActionResult> GetTrack()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var tracks = await _trackService.GetStudentTrackAsync(email);

            if (!tracks.Any())
                return BadRequest("Enroll in a track");

            return Ok(tracks);
        }

        [HttpGet("TrackRate")]
        public async Task<IActionResult> GetRate()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _trackService.GetTrackRateAsync(email);
            return Ok(result);
        }

        [HttpPut("UpdateTrack/{id}")]
        public async Task<IActionResult> UpdateTrack(int id)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _trackService.UpdateStudentTrackAsync(email, id);
            return Ok("Track updated successfully");
        }

        [HttpPut("UpdateLessonStatus")]
        public async Task<IActionResult> UpdateLessonStatus([FromBody] LessonUpdateDto model)
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var result = await _trackService.UpdateLessonStatusAsync(email, model);
            return Ok(new { Message = "Lesson updated successfully" });
        }
    }
}
