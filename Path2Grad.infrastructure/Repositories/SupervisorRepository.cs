using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class SupervisorRepository : BaseRepository<Supervisor>, ISupervisorRepository
    {
        public SupervisorRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Supervisor?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.SupervisorEmail == email);
        }
    }
}
