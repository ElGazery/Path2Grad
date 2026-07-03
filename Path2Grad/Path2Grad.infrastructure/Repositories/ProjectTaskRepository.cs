using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class ProjectTaskRepository : BaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId)
        {
            return await _dbSet
                .Where(t => t.ProjectId == projectId)
                .Include(t => t.Student)
                .ToListAsync();
        }
    }
}
