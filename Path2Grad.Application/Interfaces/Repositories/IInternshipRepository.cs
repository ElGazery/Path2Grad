using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IInternshipRepository : IRepository<Internship>
    {
        Task<IEnumerable<Internship>> GetAllWithDetailsAsync();
    }
}
