using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Infrastructure.Data;

namespace ReptiRealm_API.Application.Services.Entity
{
    public sealed class EntityServiceContext<TEntity> : IEntityServiceContext<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public EntityServiceContext(ApplicationDbContext context)
        {
            _context = context;
        }

        #region READ OPERATIONS
        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsQueryable();
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        #endregion

        #region TRANSACTION METHODS
        public void Add(TEntity entity) => _context.Set<TEntity>().Add(entity);
        public void AddRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().AddRange(entities);
        public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);
        public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);
        public void DeleteRange(IEnumerable<TEntity> entities) => _context.Set<TEntity>().RemoveRange(entities);
        #endregion

        #region SAVE CHANGES
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
        #endregion
    }
}
