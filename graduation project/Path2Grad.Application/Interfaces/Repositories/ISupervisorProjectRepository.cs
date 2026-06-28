using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface ISupervisorProjectRepository : IRepository<SupervisorProject>
    {
        Task<IEnumerable<int>> GetProjectIdsBySupervisorIdAsync(int supervisorId);
        Task<SupervisorProject?> GetByProjectAndSupervisorAsync(int projectId, int supervisorId);
    }
}
