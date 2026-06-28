using Path2Grad.Application.Dtos;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface IDoctorService
    {
        Task<object?> GetProfileAsync(string email);
        Task<IEnumerable<object>> GetProjectsAsync(string email);
        Task<object?> GetProjectByIdAsync(int id);
        Task AddRequirementAsync(ProjectRequirementCreateDto dto);
        Task<IEnumerable<object>> GetProjectFilesAsync(string email);
        Task<IEnumerable<object>> GetProjectRequestsAsync(string email);
        Task<string> HandleStatusRequestAsync(string email, SupervisorStatusRequestDto dto);
        Task DeleteRequirementAsync(int requirementId);
    }
}
