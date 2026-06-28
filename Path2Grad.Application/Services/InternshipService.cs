using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class InternshipService : IInternshipService
    {
        private readonly IInternshipRepository _internshipRepo;
        private readonly IInternshipWorkFileRepository _workFileRepo;
        private readonly IInternshipCertificateRepository _certificateRepo;

        public InternshipService(
            IInternshipRepository internshipRepo,
            IInternshipWorkFileRepository workFileRepo,
            IInternshipCertificateRepository certificateRepo)
        {
            _internshipRepo = internshipRepo;
            _workFileRepo = workFileRepo;
            _certificateRepo = certificateRepo;
        }

        public async Task<IEnumerable<object>> GetAllInternshipsAsync()
        {
            var internships = await _internshipRepo.GetAllWithDetailsAsync();
            return internships.Select(e => new
            {
                Name = e.InternshipName,
                Link = e.InternshipLink
            }).ToList();
        }

        public async Task<InternshipWorkFile> UploadWorkFileAsync(int studentId, byte[] file)
        {
            var fileEntity = new InternshipWorkFile
            {
                WorkFile = file,
                StudentId = studentId
            };
            return await _workFileRepo.AddAsync(fileEntity);
        }

        public async Task<InternshipCertificate> UploadCertificateAsync(int studentId, byte[] file)
        {
            var certEntity = new InternshipCertificate
            {
                Certificate = file,
                StudentId = studentId
            };
            return await _certificateRepo.AddAsync(certEntity);
        }

        public async Task<IEnumerable<object>> GetWorkFilesAsync(int studentId)
        {
            var files = await _workFileRepo.GetByStudentIdAsync(studentId);
            return files.Select(e => new
            {
                e.WorkFile,
                e.InternshipWorkFilesId
            }).ToList();
        }

        public async Task DeleteWorkFileAsync(int workFileId)
        {
            var file = await _workFileRepo.GetByIdAsync(workFileId);
            if (file != null)
            {
                _workFileRepo.Delete(file);
                await _workFileRepo.SaveChangesAsync();
            }
        }

        public async Task DeleteCertificateAsync(int certificateId)
        {
            var cert = await _certificateRepo.GetByIdAsync(certificateId);
            if (cert != null)
            {
                _certificateRepo.Delete(cert);
                await _certificateRepo.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<object>> GetCertificatesAsync(int studentId)
        {
            var certificates = await _certificateRepo.GetByStudentIdAsync(studentId);
            return certificates.Select(e => new
            {
                e.InternshipCertificatesId,
                e.Certificate
            }).ToList();
        }
    }
}
