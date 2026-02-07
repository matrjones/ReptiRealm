namespace ReptiRealm_API.Application.Interfaces.Entity
{
    public interface IEntityService
    {
        #region READ OPERATIONS
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : class;
        Task<TEntity?> GetByIdAsync<TEntity>(int id) where TEntity : class;
        #endregion

        #region TRANSACTION METHODS
        void Add<TEntity>(TEntity entity) where TEntity : class;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        void Delete<TEntity>(TEntity entity) where TEntity : class;
        void DeleteRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;
        #endregion

        #region SAVE CHANGES
        Task SaveChangesAsync();
        #endregion
    }
}
