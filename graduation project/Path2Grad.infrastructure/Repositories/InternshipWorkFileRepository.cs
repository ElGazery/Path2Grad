using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class InternshipWorkFileRepository : BaseRepository<InternshipWorkFile>, IInternshipWorkFileRepository
    {
        public InternshipWorkFileRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<InternshipWorkFile>> GetByStudentIdAsync(int studentId)
        {
            return await _dbSet.Where(e => e.StudentId == studentId).ToListAsync();
        }
    }
}
