using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;

namespace ReptiRealm_API.Application.Services.Entity.AccessRestriction
{
    public sealed class BypassAccessRestriction<TEntity> : IAccessRestrictionMethod<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> Filter(IQueryable<TEntity> source) => source;
    }
}