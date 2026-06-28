using Path2Grad.Application.Dtos;
using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface IStudentService
    {
        Task<Student?> GetProfileAsync(string email);
        Task SendProjectRequestAsync(int senderId, int receiverId, int? projectId);
        Task<object> GetProjectRequestsAsync(int studentId, int? currentProjectId);
        Task<Student?> HandleStatusRequestAsync(int studentId, int requestId, string status);
        Task<object?> GetProjectAsync(int studentId);
        Task AddTaskAsync(AddTaskDto dto);
        Task UploadFileAsync(int studentId, ProjectFileDto file);
        Task SendSupervisorRequestAsync(int studentId, int supervisorId, int? projectId);
        Task DeleteTaskAsync(int taskId);
    }
}
