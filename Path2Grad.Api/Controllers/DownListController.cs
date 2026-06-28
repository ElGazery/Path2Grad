using Microsoft.AspNetCore.Mvc;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownListController : ControllerBase
    {
        private readonly IDownListService _downListService;

        public DownListController(IDownListService downListService)
        {
            _downListService = downListService;
        }

        [HttpGet("ProjectField")]
        public async Task<IActionResult> GetProjectField()
        {
            var fields = await _downListService.GetProjectFieldsAsync();
            return Ok(fields);
        }

        [HttpGet("Supervisor")]
        public async Task<IActionResult> GetSupervisor()
        {
            var supervisors = await _downListService.GetSupervisorsByPositionAsync("Doctor");
            return Ok(supervisors);
        }

        [HttpGet("CoSupervisor")]
        public async Task<IActionResult> GetCoSupervisor()
        {
            var supervisors = await _downListService.GetSupervisorsByPositionAsync("TeachingAssistant");
            return Ok(supervisors);
        }

        [HttpGet("Student/{trackName}")]
        public async Task<IActionResult> GetStudentByTrack(string trackName)
        {
            var students = await _downListService.GetStudentsByTrackAsync(trackName);
            return Ok(students);
        }
    }
}
