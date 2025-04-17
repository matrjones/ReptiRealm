using ReptiRealm.Data.DAL.WorkUnits;
using ReptiRealm.Services;

namespace ReptiRealm.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            //Database Initialization
            services.AddScoped<IDbInitializer, DbInitializer>();
            services.AddScoped<IHangfireService, HangfireService>();
            services.AddScoped<ISubscriptionService, SubscriptionService>();

            //Work Units
            services.AddTransient<ReptileWorkUnit>();

            return services;
        }
    }
}
