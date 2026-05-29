using Microsoft.EntityFrameworkCore;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Application.Interfaces.Entity.Implementations;
using ReptiRealm_API.Application.Services.Entity.Configuration;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Infrastructure.Data;
using System.Linq.Expressions;
using System.Security.Authentication;

namespace ReptiRealm_API.Application.Services.Entity
{
    public sealed class EntityServiceContext<TEntity> : IEntityServiceContext<TEntity> where TEntity : class, IEntityBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Dictionary<Type, EntityConfiguration> _configuration;
        private readonly IAccessRestrictionFactory _accessRestrictionFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private User _user;

        public EntityServiceContext(
            ApplicationDbContext context, 
            IEnumerable<EntityConfiguration> configuration,
            IAccessRestrictionFactory accessRestrictionFactory,
            IHttpContextAccessor httpContextAccessor
        )
        {
            _context = context;
            _configuration = configuration.ToDictionary(s => s.EntityType);
            _accessRestrictionFactory = accessRestrictionFactory;
            _httpContextAccessor = httpContextAccessor;
            SetUser();
        }

        #region READ OPERATIONS
        public IQueryable<TEntity> GetAll(AccessRestrictionDescriptor? accessRestriction = null, bool bypassCache = false)
        {
            if(!_configuration.TryGetValue(typeof(TEntity), out var configuration))
            {
                throw new InvalidOperationException($"{typeof(TEntity)} is not configured.");
            }

            accessRestriction ??= configuration.DefaultAccessRestriction;

            IQueryable<TEntity> source;
            source = _context.Set<TEntity>();

            return _accessRestrictionFactory.Create<TEntity>(accessRestriction!).Filter(source);
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
        #endregion

        #region TRANSACTION METHODS
        public async Task Add(TEntity entity, CancellationToken cancellationToken = default)
        {
            SetEntityCreated(entity);    
            await SaveChangesAsync(cancellationToken);
        }

        public async Task AddRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            SetEntityCreated(entities);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            SetEntityUpdated(entity);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateRange(IQueryable<TEntity> entities, CancellationToken cancellationToken = default)
        {
            SetEntityUpdated(entities);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(TEntity entity, CancellationToken cancellationToken = default)
        {
            SetEntityDeleted(entity);
            await SaveChangesAsync(cancellationToken);
        }
        public async Task DeleteRange(IEnumerable<TEntity> entities, CancellationToken cancellationToken)
        {
            SetEntityDeleted(entities);
            await SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region PRIVATE METHODS
        private async Task SaveChangesAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);
        
        private void SetEntityCreated(TEntity entity)
        {
            entity.CreatedBy = _user.Id;
            // If the entity is owned by a user, set ownership automatically
            if (entity is IOwnedEntity owned)
            {
                owned.UserId = _user.Id;
            }
            entity.CreatedOn = DateTime.UtcNow;
            var entry = _context.Entry(entity);
            entry.State = EntityState.Added;
        }

        private void SetEntityCreated(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                SetEntityCreated(entity);
            }
        }

        private void SetEntityUpdated(TEntity entity)
        {
            entity.LastModifiedBy = _user.Id;
            entity.LastModifiedOn = DateTime.UtcNow;
            var entry = _context.Entry(entity);
            if(entry.State == EntityState.Detached)
            {
                entry.State = EntityState.Modified;
            }
            else
            {
                entry.DetectChanges();
            }
        }

        private void SetEntityUpdated(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                SetEntityUpdated(entity);
            }
        }
        private void SetEntityDeleted(TEntity entity)
        {
            entity.LastModifiedBy = _user.Id;
            entity.LastModifiedOn = DateTime.UtcNow;
            var entry = _context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        private void SetEntityDeleted(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                SetEntityDeleted(entity);
            }
        }

        private void SetUser()
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
            var user = _context.Set<User>().SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                throw new AuthenticationException();
            }
            _user = user;
        }
        #endregion
    }
}
