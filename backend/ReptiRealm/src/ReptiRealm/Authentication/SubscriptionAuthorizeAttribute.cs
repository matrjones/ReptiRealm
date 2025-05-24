using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ReptiRealm.Services;

namespace ReptiRealm.Authentication
{
    public class SubscriptionAuthorizeAttribute : TypeFilterAttribute
    {
        public SubscriptionAuthorizeAttribute(string requiredPlan) 
            : base(typeof(SubscriptionAuthorizationFilter))
        {
            Arguments = new object[] { requiredPlan };
        }
    }
} 