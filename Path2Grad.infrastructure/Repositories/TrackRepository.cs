using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class TrackRepository : BaseRepository<Track>, ITrackRepository
    {
        public TrackRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Track?> GetTrackWithItemsAndLessonsAsync(int trackId)
        {
            return await _dbSet
                .Include(t => t.Items)
                    .ThenInclude(i => i.ItemLessons)
                .FirstOrDefaultAsync(t => t.TrackId == trackId);
        }

        public async Task<IEnumerable<Track>> GetAllTracksAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
