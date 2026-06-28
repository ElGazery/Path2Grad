using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface ICvRepository : IRepository<Cv>
    {
        Task<IEnumerable<Cv>> GetTemplatesAsync();
    }
}
