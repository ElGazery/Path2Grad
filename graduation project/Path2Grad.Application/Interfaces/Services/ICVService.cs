using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface ICVService
    {
        Task<IEnumerable<Cv>> GetTemplatesAsync();
        Task<Cv> AddStudentCvAsync(int studentId, byte[] file, string fileName);
        Task DeleteCvAsync(int cvId);
    }
}
