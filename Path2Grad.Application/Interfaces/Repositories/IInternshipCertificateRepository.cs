using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IInternshipCertificateRepository : IRepository<InternshipCertificate>
    {
        Task<IEnumerable<InternshipCertificate>> GetByStudentIdAsync(int studentId);
    }
}
