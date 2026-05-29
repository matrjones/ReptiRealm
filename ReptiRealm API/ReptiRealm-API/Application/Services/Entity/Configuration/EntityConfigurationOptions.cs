namespace ReptiRealm_API.Application.Services.Entity.Configuration
{
    public class EntityConfigurationOptions
    {
        public readonly Dictionary<Type, EntityConfigurationBuilder> _builders = new();

        public EntityConfigurationBuilder<TEntity> Entity<TEntity>() where TEntity : class
        {
            if (!_builders.TryGetValue(typeof(TEntity), out var builder))
            {
                builder = new EntityConfigurationBuilder<TEntity>();
                _builders.Add(typeof(TEntity), builder);
            }
            return (EntityConfigurationBuilder<TEntity>)builder;
        }

        public IEnumerable<EntityConfiguration> Build()
            => _builders.Values.Select(b => b.Build());
    }
}
