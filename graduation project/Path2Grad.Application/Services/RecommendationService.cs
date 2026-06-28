using Path2Grad.Application.Helpers;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class RecommendationService : IRecommendationService
    {
        public Task<string> GetRecommendedTrackAsync(SurveyResponse surveyResponse)
        {
            var matcher = new CareerTrackMatcher();
            var recommendedTrack = matcher.DetermineCareerTrack(surveyResponse);
            return Task.FromResult(recommendedTrack.ToString());
        }
    }
}
