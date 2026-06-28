using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IStudentProjectJoinRequestRepository : IRepository<StudentProjectJoinRequest>
    {
        Task<IEnumerable<StudentProjectJoinRequest>> GetByStudentIdAsync(int studentId);
        Task<StudentProjectJoinRequest?> GetByIdWithDetailsAsync(int requestId);
    }
}
