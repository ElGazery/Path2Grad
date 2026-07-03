using Path2Grad.Application.Dtos;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<object>> GetAllTracksAsync();
        Task<string> AddTrackToStudentAsync(string email, string trackName);
        Task<IEnumerable<object>> GetStudentTrackAsync(string email);
        Task<object> GetTrackRateAsync(string email);
        Task<string> UpdateStudentTrackAsync(string email, int trackId);
        Task<string> UpdateLessonStatusAsync(string email, LessonUpdateDto dto);
    }
}
