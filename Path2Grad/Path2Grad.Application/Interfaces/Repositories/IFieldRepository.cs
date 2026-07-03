using Path2Grad.Domain.Entities;

namespace Path2Grad.Application.Interfaces.Repositories
{
    public interface IFieldRepository : IRepository<Field>
    {
        Task<IEnumerable<Field>> GetFieldsByNamesAsync(List<string> fieldNames);
    }
}
