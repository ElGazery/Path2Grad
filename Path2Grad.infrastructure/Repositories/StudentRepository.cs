using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Student?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(s => s.StudentEmail == email);
        }

        public async Task<Student?> GetStudentWithProjectAsync(int studentId)
        {
            return await _dbSet
                .Include(s => s.Project)
                    .ThenInclude(p => p.Requirements)
                .Include(s => s.Project)
                    .ThenInclude(p => p.projectFiles)
                .Include(s => s.Project)
                    .ThenInclude(p => p.Tasks)
                        .ThenInclude(t => t.Student)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);
        }
    }
}
