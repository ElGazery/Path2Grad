using Path2Grad.Application.Dtos;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Application.Interfaces.Services;

namespace Path2Grad.Application.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRepository _trackRepo;
        private readonly IStudentRepository _studentRepo;

        public TrackService(ITrackRepository trackRepo, IStudentRepository studentRepo)
        {
            _trackRepo = trackRepo;
            _studentRepo = studentRepo;
        }

        public async Task<IEnumerable<object>> GetAllTracksAsync()
        {
            var tracks = await _trackRepo.GetAllTracksAsync();
            return tracks.Select(e => new { e.TrackId, e.TrackName }).ToList();
        }

        public async Task<string> AddTrackToStudentAsync(string email, string trackName)
        {
            var student = await _studentRepo.GetByEmailAsync(email);
            if (student == null) return "Student not found";

            var tracks = await _trackRepo.FindAsync(e => e.TrackName == trackName);
            var track = tracks.FirstOrDefault();
            if (track == null) return "Track not found";

            student.TrackId = track.TrackId;
            _studentRepo.Update(student);
            return "Track assigned successfully";
        }

        public async Task<IEnumerable<object>> GetStudentTrackAsync(string email)
        {
            var student = await _studentRepo.GetByEmailAsync(email);
            if (student?.TrackId == null) return Enumerable.Empty<object>();

            var track = await _trackRepo.GetTrackWithItemsAndLessonsAsync(student.TrackId.Value);
            if (track == null) return Enumerable.Empty<object>();

            return new List<object>
            {
                new
                {
                    TrackName = track.TrackName,
                    Items = track.Items?.Select(i => new
                    {
                        ItemName = i.Name,
                        Lessons = i.ItemLessons?.Select(l => new
                        {
                            LessonId = l.Id,
                            LessonName = l.Name,
                            IsComplet = l.IsComplet
                        }).ToList()
                    }).ToList()
                }
            };
        }

        public async Task<object> GetTrackRateAsync(string email)
        {
            var student = await _studentRepo.GetByEmailAsync(email);
            if (student?.TrackId == null) return new { Message = "No track assigned", PercentComplete = 0 };

            var track = await _trackRepo.GetTrackWithItemsAndLessonsAsync(student.TrackId.Value);
            if (track?.Items == null) return new { Message = "Track Rate", PercentComplete = 0 };

            var totalLessons = track.Items.SelectMany(i => i.ItemLessons ?? new List<Domain.Entities.ItemLesson>()).Count();
            var completedLessons = track.Items.SelectMany(i => i.ItemLessons ?? new List<Domain.Entities.ItemLesson>())
                .Count(l => l.IsComplet == true);

            double percentComplete = totalLessons > 0 ? (double)completedLessons / totalLessons * 100 : 0;
            return new { Message = "Track Rate", PercentComplete = percentComplete };
        }

        public async Task<string> UpdateStudentTrackAsync(string email, int trackId)
        {
            var student = await _studentRepo.GetByEmailAsync(email);
            if (student == null) return "Student not found";

            student.TrackId = trackId;
            _studentRepo.Update(student);
            return "Track updated successfully";
        }

        public async Task<string> UpdateLessonStatusAsync(string email, LessonUpdateDto dto)
        {
            var student = await _studentRepo.GetByEmailAsync(email);
            if (student?.TrackId == null) return "No track assigned";

            var track = await _trackRepo.GetTrackWithItemsAndLessonsAsync(student.TrackId.Value);
            var lesson = track?.Items?
                .SelectMany(i => i.ItemLessons ?? new List<Domain.Entities.ItemLesson>())
                .FirstOrDefault(l => l.Id == dto.LessonId);

            if (lesson == null) return "Lesson not found";

            lesson.IsComplet = dto.IsComplet;
            return "Lesson updated successfully";
        }
    }
}
