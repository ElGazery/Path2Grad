using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Path2Grad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class RecommendationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecommendationController(ApplicationDbContext context)
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

        [HttpPost("Survey")]
        public async Task<IActionResult> PostSurvey(SurveyResponse surveyResponse)
        {
            var student = await GetCurrentStudentAsync();
            var matcher = new CareerTrackMatcher();
            var recommendedTrack = matcher.DetermineCareerTrack(surveyResponse);

            return Ok(recommendedTrack.ToString());
        }
    }
}