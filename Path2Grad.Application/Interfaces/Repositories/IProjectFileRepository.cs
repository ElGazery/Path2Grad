using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IProjectFileRepository : IRepository<ProjectFile>
    {
        Task<IEnumerable<ProjectFile>> GetByProjectIdsAsync(IEnumerable<int> projectIds);
    }
}
