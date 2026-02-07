using System.Linq;

namespace ReptiRealm_API.Application.Interfaces.Entity
{
    public interface IEntityServiceContext<TEntity> where TEntity : class
    {
        #region READ OPERATIONS
        IQueryable<TEntity> GetAll();
        Task<TEntity?> GetByIdAsync(int id);
        #endregion

        #region TRANSACTION METHODS
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        #endregion

        #region SAVE CHANGES
        Task SaveChangesAsync();
        #endregion
    }
}
