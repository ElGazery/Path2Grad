using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class ProjectBankRepository : BaseRepository<ProjectsBank>, IProjectBankRepository
    {
        public ProjectBankRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<ProjectsBank?> GetProjectWithFieldsAsync(int id)
        {
            return await _dbSet
                .Include(p => p.ProjectsBankProjectFields)
                .FirstOrDefaultAsync(p => p.ProjectBankId == id);
        }
    }
}
