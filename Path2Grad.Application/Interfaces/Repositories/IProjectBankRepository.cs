using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IProjectBankRepository : IRepository<ProjectsBank>
    {
        Task<ProjectsBank?> GetProjectWithFieldsAsync(int id);
    }
}
