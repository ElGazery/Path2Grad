using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class ProjectFileRepository : BaseRepository<ProjectFile>, IProjectFileRepository
    {
        public ProjectFileRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProjectFile>> GetByProjectIdsAsync(IEnumerable<int> projectIds)
        {
            return await _dbSet
                .Where(pf => projectIds.Contains(pf.ProjectId))
                .ToListAsync();
        }
    }
}
