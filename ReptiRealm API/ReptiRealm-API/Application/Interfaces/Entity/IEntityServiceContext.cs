using ReptiRealm_API.Domain.Entities.Common;
using System.Linq.Expressions;

namespace ReptiRealm_API.Application.Interfaces.Entity
{
    public interface IEntityServiceContext<TEntity> where TEntity : class, IEntityBase
    {
        #region READ OPERATIONS
        IQueryable<TEntity> GetAll(AccessRestrictionDescriptor? accessRestriction = null, bool bypassCache = false);
        Task<TEntity?> GetByIdAsync(Guid id);
        #endregion

        #region TRANSACTION METHODS
        Task Add(TEntity entity, CancellationToken cancellationToken = default);
        Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        Task Update(TEntity entity, CancellationToken cancellationToken = default);
        Task UpdateRange(IQueryable<TEntity> entities, CancellationToken cancellationToken = default);
        Task Delete(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
        #endregion
    }
}
