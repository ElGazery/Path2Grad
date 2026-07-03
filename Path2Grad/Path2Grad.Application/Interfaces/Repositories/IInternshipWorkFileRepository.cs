using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IInternshipWorkFileRepository : IRepository<InternshipWorkFile>
    {
        Task<IEnumerable<InternshipWorkFile>> GetByStudentIdAsync(int studentId);
    }
}
