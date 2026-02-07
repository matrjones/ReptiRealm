using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Infrastructure.Data;

namespace ReptiRealm_API.Application.Services.Entity
{
    public sealed class EntityService : IEntityService
    {
        private readonly ApplicationDbContext _context;

        public EntityService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Entry point for fluent-style API: EntityService.For<Entity>()
        /// </summary>
        public IEntityServiceContext<TEntity> For<TEntity>() where TEntity : class
        {
            return new EntityServiceContext<TEntity>(_context);
        }
    }
}
