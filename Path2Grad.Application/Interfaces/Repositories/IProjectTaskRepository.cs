using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IProjectTaskRepository : IRepository<ProjectTask>
    {
        Task<IEnumerable<ProjectTask>> GetByProjectIdAsync(int projectId);
    }
}
