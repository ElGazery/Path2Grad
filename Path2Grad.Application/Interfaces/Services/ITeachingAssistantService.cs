using Path2Grad.Application.Dtos;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface ITeachingAssistantService
    {
        Task<object?> GetProfileAsync(string email);
        Task<IEnumerable<object>> GetProjectsAsync(string email);
        Task<object?> GetProjectByIdAsync(int id);
        Task AddRequirementAsync(ProjectRequirementCreateDto dto);
        Task<IEnumerable<object>> GetProjectRequestsAsync(string email);
        Task<string> HandleStatusRequestAsync(string email, SupervisorStatusRequestDto dto);
        Task DeleteRequirementAsync(int requirementId);
    }
}
