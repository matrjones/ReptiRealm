using Microsoft.Extensions.Caching.Memory;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;
using ReptiRealm_API.Application.Services.Entity.AccessRestriction;
using ReptiRealm_API.Application.Services.Entity.Configuration;

namespace ReptiRealm_API.Application.Services.Entity;

public class EntityConfigurationBuilder<TEntity> : EntityConfigurationBuilder where TEntity : class
{
    private AccessRestrictionDescriptor? _defaultAccessRestriction;
    public EntityConfigurationBuilder<TEntity> AccessRestriction(AccessRestrictionDescriptor accessRestriction)
    {
        _defaultAccessRestriction = accessRestriction;
        return this;
    }

    public EntityConfigurationBuilder<TEntity> AccessRestriction<TAccessRestriction>() where TAccessRestriction : IAccessRestrictionMethod<TEntity>
        => AccessRestriction(new AccessRestrictionDescriptor(typeof(TAccessRestriction)));

    public EntityConfigurationBuilder<TEntity> BypassAccessRestriction()
        => AccessRestriction<BypassAccessRestriction<TEntity>>();

    private bool _isCached;
    private CacheItemPriority? _cachePriority;
    private TimeSpan? _slidingExpiration;
    private TimeSpan? _cacheDuration;

    public EntityConfigurationBuilder<TEntity> Cache(CacheItemPriority priority = CacheItemPriority.Low, TimeSpan? duration = null, TimeSpan? slidingExpiration = null)
    {
        _cachePriority = priority;
        _isCached = true;
        _cacheDuration = duration ?? TimeSpan.FromMinutes(30);
        _slidingExpiration = slidingExpiration;

        return this;
    }

    public override EntityConfiguration Build() => new()
    {
        EntityType = typeof(TEntity),
        DefaultAccessRestriction = _defaultAccessRestriction,
        IsCached = _isCached,
        CachePriority = _cachePriority,
        SlidingExpiration = _slidingExpiration,
        CacheDuration = _cacheDuration
    };
}

public abstract class EntityConfigurationBuilder
{
    public abstract EntityConfiguration Build();
}
