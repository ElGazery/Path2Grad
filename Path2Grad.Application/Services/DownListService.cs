using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Application.Services
{
    public class DownListService : IDownListService
    {
        private readonly IFieldRepository _fieldRepo;
        private readonly ISupervisorRepository _supervisorRepo;
        private readonly IStudentRepository _studentRepo;

        public DownListService(
            IFieldRepository fieldRepo,
            ISupervisorRepository supervisorRepo,
            IStudentRepository studentRepo)
        {
            _fieldRepo = fieldRepo;
            _supervisorRepo = supervisorRepo;
            _studentRepo = studentRepo;
        }

        public async Task<IEnumerable<object>> GetProjectFieldsAsync()
        {
            var fields = await _fieldRepo.GetAllAsync();
            return fields;
        }

        public async Task<IEnumerable<object>> GetSupervisorsByPositionAsync(string position)
        {
            var supervisors = await _supervisorRepo.FindAsync(e => e.Position == position);
            return supervisors.Select(s => new
            {
                s.SupervisorId,
                s.SupervisorName,
                s.Pic,
                s.Specialization
            }).ToList();
        }

        public async Task<IEnumerable<object>> GetStudentsByTrackAsync(string trackName)
        {
            var students = await _studentRepo.FindAsync(s => s.Track.TrackName == trackName);
            return students.Select(e => new
            {
                e.StudentId,
                e.StudentName,
                track = e.Track?.TrackName,
                e.Pic
            }).ToList();
        }
    }
}
