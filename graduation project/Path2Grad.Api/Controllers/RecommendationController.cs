using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Path2Grad.Domain.Entities;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RecommendationController : ControllerBase
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }

        [HttpPost("Survey")]
        public async Task<IActionResult> PostSurvey(SurveyResponse surveyResponse)
        {
            var recommendedTrack = await _recommendationService.GetRecommendedTrackAsync(surveyResponse);
            return Ok(recommendedTrack);
        }
    }
}
