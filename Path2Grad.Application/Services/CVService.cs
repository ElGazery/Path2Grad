using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Services
{
    public class CVService : ICVService
    {
        private readonly ICvRepository _cvRepo;

        public CVService(ICvRepository cvRepo)
        {
            _cvRepo = cvRepo;
        }

        public async Task<IEnumerable<Cv>> GetTemplatesAsync()
        {
            return await _cvRepo.GetTemplatesAsync();
        }

        public async Task DeleteCvAsync(int cvId)
        {
            var cv = await _cvRepo.GetByIdAsync(cvId);
            if (cv != null)
            {
                _cvRepo.Delete(cv);
                await _cvRepo.SaveChangesAsync();
            }
        }

        public async Task<Cv> AddStudentCvAsync(int studentId, byte[] file, string fileName)
        {
            var studentCv = new Cv
            {
                Cvfile = file,
                StudentId = studentId,
                Type = "StudentCV",
                CVName = fileName
            };

            await _cvRepo.AddAsync(studentCv);
            await _cvRepo.SaveChangesAsync();
            return studentCv;
        }
    }
}
