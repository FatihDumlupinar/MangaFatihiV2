using MangaFatihi.Domain.Common;
using System.Linq.Expressions;

namespace MangaFatihi.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        Task<T?> FindOneAsync(Expression<Func<T?, bool>> expression, CancellationToken cancellationToken = default);

        IQueryable<T> Find(Expression<Func<T, bool>>? expression = default);

        ValueTask<T> AddAsyncReturnEntity(T entity, CancellationToken cancellationToken = default);

        ValueTask AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    }
}
