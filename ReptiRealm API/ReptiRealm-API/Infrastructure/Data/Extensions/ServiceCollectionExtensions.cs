using Microsoft.Extensions.Options;
using ReptiRealm_API.Application.Interfaces.Entity;
using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;
using ReptiRealm_API.Application.Interfaces.Entity.Implementations;
using ReptiRealm_API.Application.Services.Entity;
using ReptiRealm_API.Application.Services.Entity.Configuration;
using ReptiRealm_API.Application.Services.Entity.Implementations;
using System.Reflection;

namespace ReptiRealm_API.Infrastructure.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers application services for dependency injection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register the EntityService and its supporting dependencies
            services.AddScoped<IEntityService, EntityService>();

            // AccessRestrictionFactory is stateless and can be a singleton
            services.AddScoped<IAccessRestrictionFactory, AccessRestrictionFactory>()
                .AddAccessRestrictionMethods(typeof(IAccessRestrictionFactory).Assembly);

            // Register an empty entity configuration list by default. Consume code can replace or populate this as needed.
            services.AddSingleton<IEnumerable<EntityConfiguration>>(s => s.GetRequiredService<IOptions<EntityConfigurationOptions>>().Value.Build().ToList());

            // You can add other services here in the future
            // e.g., services.AddScoped<IUserService, UserService>();

            return services;
        }

        public static IServiceCollection ConfigureEntities(this IServiceCollection services, Action<EntityConfigurationOptions> options)
            => services.Configure(options);

        public static IServiceCollection AddAccessRestrictionMethods(this IServiceCollection services, Assembly assembly, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            foreach(var t in assembly.GetTypes())
            {
                if(!t.IsAbstract && t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IAccessRestrictionMethod<>)))
                {
                    services.Add(new ServiceDescriptor(t, t, lifetime));
                }
            }
            return services;
        }
    }
}
