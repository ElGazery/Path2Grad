using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<Project?> GetProjectWithDetailsAsync(int projectId);
        Task<IEnumerable<Project>> GetAllProjectsWithDetailsAsync();
    }
}
