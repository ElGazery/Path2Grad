using Microsoft.EntityFrameworkCore;
using Path2Grad.Application.Interfaces.Repositories;
using Path2Grad.Domain.Entities;
using Path2Grad.Infrastructure.Data;

namespace Path2Grad.Infrastructure.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        public FieldRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Field>> GetFieldsByNamesAsync(List<string> fieldNames)
        {
            return await _dbSet
                .Where(f => fieldNames.Contains(f.FieldName))
                .ToListAsync();
        }
    }
}
