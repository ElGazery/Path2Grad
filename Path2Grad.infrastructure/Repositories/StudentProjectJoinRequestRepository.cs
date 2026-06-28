using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class StudentProjectJoinRequestRepository : BaseRepository<StudentProjectJoinRequest>, IStudentProjectJoinRequestRepository
    {
        public StudentProjectJoinRequestRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<StudentProjectJoinRequest>> GetByStudentIdAsync(int studentId)
        {
            return await _dbSet
                .Where(e => e.StudentId == studentId)
                .Include(e => e.Sender)
                .Include(e => e.Project)
                .ToListAsync();
        }

        public async Task<StudentProjectJoinRequest?> GetByIdWithDetailsAsync(int requestId)
        {
            return await _dbSet
                .Include(e => e.Project)
                .Include(e => e.Sender)
                .FirstOrDefaultAsync(e => e.RequestId == requestId);
        }
    }
}
