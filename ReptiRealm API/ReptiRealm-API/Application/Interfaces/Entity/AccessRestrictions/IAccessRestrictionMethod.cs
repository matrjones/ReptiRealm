namespace ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions
{
    public interface IAccessRestrictionMethod<TEntity>
    {
        IQueryable<TEntity> Filter(IQueryable<TEntity> source);
    }
}
