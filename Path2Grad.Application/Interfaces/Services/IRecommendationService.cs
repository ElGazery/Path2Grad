using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface IRecommendationService
    {
        Task<string> GetRecommendedTrackAsync(SurveyResponse surveyResponse);
    }
}
