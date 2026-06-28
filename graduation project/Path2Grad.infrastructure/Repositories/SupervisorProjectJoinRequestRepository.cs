using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class SupervisorProjectJoinRequestRepository : BaseRepository<SupervisorProjectJoinRequest>, ISupervisorProjectJoinRequestRepository
    {
        public SupervisorProjectJoinRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SupervisorProjectJoinRequest>> GetBySupervisorIdAsync(int supervisorId)
        {
            return await _dbSet
                .Where(e => e.SupervisorId == supervisorId)
                .Include(e => e.Project)
                .Include(e => e.Supervisor)
                .ToListAsync();
        }
    }
}
