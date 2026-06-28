using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class InternshipRepository : BaseRepository<Internship>, IInternshipRepository
    {
        public InternshipRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Internship>> GetAllWithDetailsAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
