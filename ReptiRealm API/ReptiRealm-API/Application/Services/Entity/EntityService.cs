using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Application.Interfaces.Entity.Implementations;
using ReptiRealm_API.Application.Services.Entity.Configuration;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Infrastructure.Data;

namespace ReptiRealm_API.Application.Services.Entity
{
    public sealed class EntityService : IEntityService
    {
        private readonly ApplicationDbContext _context;
        private readonly IEnumerable<EntityConfiguration> _configuration;
        private readonly IAccessRestrictionFactory _accessRestrictionFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EntityService(
            ApplicationDbContext context,
            IEnumerable<EntityConfiguration> configuration,
            IAccessRestrictionFactory accessRestrictionFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _configuration = configuration;
            _accessRestrictionFactory = accessRestrictionFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Entry point for fluent-style API: EntityService.For<Entity>()
        /// </summary>
        public IEntityServiceContext<TEntity> For<TEntity>() where TEntity : class, IEntityBase
            =>  new EntityServiceContext<TEntity>(_context, _configuration, _accessRestrictionFactory, _httpContextAccessor);
    }
}
