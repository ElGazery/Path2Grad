using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface IInternshipService
    {
        Task<IEnumerable<object>> GetAllInternshipsAsync();
        Task<InternshipWorkFile> UploadWorkFileAsync(int studentId, byte[] file);
        Task<InternshipCertificate> UploadCertificateAsync(int studentId, byte[] file);
        Task<IEnumerable<object>> GetWorkFilesAsync(int studentId);
        Task<IEnumerable<object>> GetCertificatesAsync(int studentId);
        Task DeleteWorkFileAsync(int workFileId);
        Task DeleteCertificateAsync(int certificateId);
    }
}
