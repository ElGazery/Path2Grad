using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<Student?> GetByEmailAsync(string email);
        Task<Student?> GetStudentWithProjectAsync(int studentId);
    }
}
