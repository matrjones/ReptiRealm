using Microsoft.Extensions.Caching.Memory;
using ReptiRealm_API.Application.Interfaces.Entity;

namespace ReptiRealm_API.Application.Services.Entity.Configuration
{
    public class EntityConfiguration
    {
        public required Type EntityType { get; init; }
        public AccessRestrictionDescriptor? DefaultAccessRestriction {  get; init; }
        public required bool IsCached { get; init; }
        public CacheItemPriority? CachePriority { get; init; }
        public TimeSpan? SlidingExpiration { get; init; }
        public TimeSpan? CacheDuration { get; init; }
    }
}
