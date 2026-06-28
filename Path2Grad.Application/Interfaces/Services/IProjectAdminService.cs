namespace Path2Grad.Application.Interfaces.Services
{
    public interface IProjectAdminService
    {
        Task<object?> GetProfileAsync(string email);
        Task<IEnumerable<object>> GetAllProjectsAsync();
        Task<object?> GetProjectByIdAsync(int id);
        Task DeleteProjectAsync(int projectId);
    }
}
