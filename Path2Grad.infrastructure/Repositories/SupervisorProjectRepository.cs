using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class SupervisorProjectRepository : BaseRepository<SupervisorProject>, ISupervisorProjectRepository
    {
        public SupervisorProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<int>> GetProjectIdsBySupervisorIdAsync(int supervisorId)
        {
            return await _dbSet
                .Where(sp => sp.SupervisorId == supervisorId)
                .Select(sp => sp.ProjectId)
                .ToListAsync();
        }

        public async Task<SupervisorProject?> GetByProjectAndSupervisorAsync(int projectId, int supervisorId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(sp => sp.ProjectId == projectId && sp.SupervisorId == supervisorId);
        }
    }
}
