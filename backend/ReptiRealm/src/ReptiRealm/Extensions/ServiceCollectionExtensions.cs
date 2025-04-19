using Microsoft.Extensions.DependencyInjection;
using ReptiRealm.Services;
using ReptiRealm.Data.DAL.WorkUnits;
using ReptiRealm.Data.DAL.Repository;
using ReptiRealm.Data;

namespace ReptiRealm.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            // Register work units
            services.AddScoped<SubscriptionWorkUnit>();
            
            // Register services
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            return services;
        }
    }
} 