using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class CvRepository : BaseRepository<Cv>, ICvRepository
    {
        public CvRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Cv>> GetTemplatesAsync()
        {
            return await _dbSet.Where(e => e.Type == "Templet").ToListAsync();
        }
    }
}
