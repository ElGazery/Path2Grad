namespace Path2Grad.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<object?> LoginAsync(string email, string password, string role);
        Task<string> GenerateJwtTokenAsync(string email, string role);
    }
}
