using MangaFatihi.Domain.Common;
using MangaFatihi.Domain.Interfaces;
using MangaFatihi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MangaFatihi.Application.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        #region Ctor&Fields

        protected readonly WriteDbContext _writeDbContext;
        protected readonly ReadOnlyDbContext _readOnlyDbContext;

        public GenericRepository(WriteDbContext writeDbContext, ReadOnlyDbContext readOnlyDbContext)
        {
            _writeDbContext = writeDbContext;
            _readOnlyDbContext = readOnlyDbContext;
        }

        #endregion

        public virtual async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _ = await _writeDbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public virtual async Task<T> AddAsyncReturnEntity(T entity, CancellationToken cancellationToken = default)
        {
            var data = await _writeDbContext.Set<T>().AddAsync(entity, cancellationToken);
            return data.Entity;
        }

        public virtual async Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await _writeDbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                entity.IsActive = false;
                _ = _writeDbContext.Set<T>().Update(entity);
            }, cancellationToken);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    entity.IsActive = false;
                    _ = _writeDbContext.Set<T>().Update(entity);
                }
            }, cancellationToken);
        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>>? expression = default)
        {
            return expression == null ? _readOnlyDbContext.Set<T>() : _readOnlyDbContext.Set<T>().Where(expression);
        }

        public virtual Task<T?> FindOneAsync(Expression<Func<T?, bool>> expression, CancellationToken cancellationToken = default)
        {
            return _readOnlyDbContext.Set<T>().FirstOrDefaultAsync(expression, cancellationToken);
        }

        public virtual Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return _readOnlyDbContext.Set<T>().FirstOrDefaultAsync(i => i.IsActive && i.Id == id, cancellationToken);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                _ = _writeDbContext.Set<T>().Update(entity);
            }, cancellationToken);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            await Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    _ = _writeDbContext.Set<T>().Update(entity);
                }
            }, cancellationToken);
        }
    }
}
