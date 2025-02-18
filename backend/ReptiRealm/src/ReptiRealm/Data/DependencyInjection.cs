using ReptiRealm.Data.DAL.WorkUnits;

namespace ReptiRealm.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            //Database Initialization
            services.AddScoped<IDbInitializer, DbInitializer>();

            //Work Units
            services.AddTransient<ReptileWorkUnit>();

            return services;
        }
    }
}
