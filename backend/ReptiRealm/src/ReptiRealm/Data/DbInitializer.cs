using Microsoft.AspNetCore.Identity;
using ReptiRealm.Authentication;

namespace ReptiRealm.Data
{
    public interface IDbInitializer
    {
        void Initialize();
    }
    public class DbInitializer : IDbInitializer
    {
        private readonly IConfiguration configuration;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DbInitializer(IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.configuration = configuration;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public void Initialize()
        {
            string superadminEmail = configuration.GetValue<string>("SuperUser:Email");
            string superadminUsername = configuration.GetValue<string>("SuperUser:Username");
            string superadminPassword = configuration.GetValue<string>("SuperUser:Password");
            string DefaultRoles = configuration.GetValue<string>("DefaultRoles");
            ApplicationUser? user = userManager.FindByEmailAsync(superadminEmail).Result;
            if (user == null)
            {

                user = new ApplicationUser
                {
                    UserName = superadminUsername,
                    Email = superadminEmail,
                    EmailConfirmed = true,
                };
                _ = userManager.CreateAsync(user, superadminPassword).Result;
            }
            string[] roles = DefaultRoles.Split(';');
            foreach (var r in roles)
            {
                IdentityRole? newRole = roleManager.FindByNameAsync(r).Result;
                if (newRole == null)
                {
                    newRole = new IdentityRole
                    {
                        Name = r
                    };
                    _ = roleManager.CreateAsync(newRole).Result;
                }
            }
            user = userManager.FindByEmailAsync(superadminEmail).Result;

            foreach (var r in roles)
            {
                if (user != null && !userManager.IsInRoleAsync(user, r).Result)
                {
                    _ = userManager.AddToRoleAsync(user, r).Result;
                }
            }
        }
    }
}
