using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Infrastructure.Data.Extensions;
using ReptiRealm_API.Application.Services.Entity.Configuration;
using ReptiRealm_API.Application.Services.Entity.AccessRestriction;
using ReptiRealm_API.Domain.Entities.Common;

namespace ReptiRealm_API.Application.Services.Entity.Configuration
{
    public static class EntityConfigurationExtensions
    {
        public static IServiceCollection ConfigureEntities(this IServiceCollection services) => services.ConfigureEntities(builder =>
        {
            builder.Entity<Defecation>().BypassAccessRestriction();
            builder.Entity<Feed>().BypassAccessRestriction();
            builder.Entity<FoodType>().BypassAccessRestriction();
            builder.Entity<Morph>().BypassAccessRestriction();
            builder.Entity<Regurgitation>().BypassAccessRestriction();
            builder.Entity<Reptile>().AccessRestriction<AccessibleReptilesRestriction>();
            builder.Entity<Shed>().BypassAccessRestriction();
            builder.Entity<Species>().BypassAccessRestriction();
            builder.Entity<User>().BypassAccessRestriction();
            builder.Entity<Weight>().BypassAccessRestriction();
        });
    }
}
