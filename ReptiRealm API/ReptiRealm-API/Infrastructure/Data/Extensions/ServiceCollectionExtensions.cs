using ReptiRealm_API.Application.Services.Entity;

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
            // Register the EntityService
            services.AddScoped<IEntityService, EntityService>();

            // You can add other services here in the future
            // e.g., services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
