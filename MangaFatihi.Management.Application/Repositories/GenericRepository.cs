using MangaFatihi.Shared.Domain.Common;
using MangaFatihi.Shared.Domain.Interfaces;
using MangaFatihi.Shared.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MangaFatihi.Management.Application.Repositories
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

        public virtual async ValueTask AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            _ = await _writeDbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        public virtual async ValueTask<T> AddAsyncReturnEntity(T entity, CancellationToken cancellationToken = default)
        {
            var data = await _writeDbContext.Set<T>().AddAsync(entity, cancellationToken);
            return data.Entity;
        }

        public virtual Task AddRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return _writeDbContext.Set<T>().AddRangeAsync(entities, cancellationToken);
        }

        public virtual Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                entity.IsActive = false;
                _ = _writeDbContext.Set<T>().Update(entity);
            }, cancellationToken);
        }

        public virtual Task DeleteRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
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

        public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                _ = _writeDbContext.Set<T>().Update(entity);
            }, cancellationToken);
        }

        public virtual Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                foreach (var entity in entities)
                {
                    _ = _writeDbContext.Set<T>().Update(entity);
                }
            }, cancellationToken);
        }
    }
}
