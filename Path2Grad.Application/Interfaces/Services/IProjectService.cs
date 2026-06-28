using Path2Grad.Application.Dtos;

namespace Path2Grad.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<object>> GetProjectBankAsync();
        Task<object?> GetProjectBankByIdAsync(int id);
        Task<string> CustomizeProjectAsync(string email, ProjectDto projectDto);
    }
}
