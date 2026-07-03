using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface ITrackRepository : IRepository<Track>
    {
        Task<Track?> GetTrackWithItemsAndLessonsAsync(int trackId);
        Task<IEnumerable<Track>> GetAllTracksAsync();
    }
}
