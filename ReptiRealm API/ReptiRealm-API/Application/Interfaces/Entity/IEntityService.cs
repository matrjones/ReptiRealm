namespace ReptiRealm_API.Application.Interfaces.Entity
{
    public interface IEntityService
    {
        /// <summary>
        /// Entry point to get a typed context for the entity
        /// </summary>
        IEntityServiceContext<TEntity> For<TEntity>() where TEntity : class;
    }
}
