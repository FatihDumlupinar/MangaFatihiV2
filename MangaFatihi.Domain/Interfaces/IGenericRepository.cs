using MangaFatihi.Domain.Common;
using System.Linq.Expressions;

namespace MangaFatihi.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        T? GetById(Guid id);

        Task<T?> FindOneAsync(Expression<Func<T?, bool>> expression, CancellationToken cancellationToken = default);
        T? FindOne(Expression<Func<T?, bool>> expression);

        IQueryable<T> Find(Expression<Func<T, bool>> expression);
        IQueryable<T> FindAll();

        Task<T> AddAsyncReturnEntity(T entity, CancellationToken cancellationToken = default);

        Task AddAsync(T entity, CancellationToken cancellationToken = default);
        Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

        Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);

    }
}
