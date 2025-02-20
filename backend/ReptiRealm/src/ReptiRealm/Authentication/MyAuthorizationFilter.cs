using Hangfire.Dashboard;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ReptiRealm.Authentication
{
    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        { 
            var httpContext = context.GetHttpContext();

            
            if (httpContext.Request.Cookies["token"] != null)
            {
                string jwtCookie = httpContext.Request.Cookies["token"];
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                JwtSecurityToken securityToken = handler.ReadToken(jwtCookie) as JwtSecurityToken;
                // return true or false based on the presence of a specific claim e.g role claim
                // string role = securityToken.Claims.First(claim =&gt; claim.Type == "role").Value;
                // return role == "THE_ROLE_WE_ARE_LOOKING_FOR";
                foreach (var item in securityToken.Claims.Where(x => x.Type == ClaimTypes.Role))
                {
                    if(item.Value == "Admin")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
