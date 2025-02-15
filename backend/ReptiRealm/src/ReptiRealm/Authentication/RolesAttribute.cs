using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ReptiRealm.Authentication
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
