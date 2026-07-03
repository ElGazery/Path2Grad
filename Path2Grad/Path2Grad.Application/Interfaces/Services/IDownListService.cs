namespace Path2Grad.Application.Interfaces.Services
{
    public interface IDownListService
    {
        Task<IEnumerable<object>> GetProjectFieldsAsync();
        Task<IEnumerable<object>> GetSupervisorsByPositionAsync(string position);
        Task<IEnumerable<object>> GetStudentsByTrackAsync(string trackName);
    }
}
