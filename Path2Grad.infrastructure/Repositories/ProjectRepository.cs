using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<Project?> GetProjectWithDetailsAsync(int projectId)
        {
            return await _dbSet
                .Include(p => p.Students)
                    .ThenInclude(s => s.Supervisors)
                .Include(p => p.ProjectFields)
                .Include(p => p.SupervisorProjects)
                    .ThenInclude(sp => sp.Supervisor)
                .Include(p => p.Tasks)
                    .ThenInclude(t => t.Student)
                .Include(p => p.TeamMembers)
                .Include(p => p.Requirements)
                .Include(p => p.projectFiles)
                .FirstOrDefaultAsync(p => p.ProjectId == projectId);
        }

        public async Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync()
        {
            return await _dbSet
                .Include(p => p.Students)
                .Include(p => p.SupervisorProjects)
                    .ThenInclude(sp => sp.Supervisor)
                .Include(p => p.ProjectFields)
                .ToListAsync();
        }
    }
}
