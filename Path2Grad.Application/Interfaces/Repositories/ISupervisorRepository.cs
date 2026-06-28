using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface ISupervisorRepository : IRepository<Supervisor>
    {
        Task<Supervisor?> GetByEmailAsync(string email);
    }
}
