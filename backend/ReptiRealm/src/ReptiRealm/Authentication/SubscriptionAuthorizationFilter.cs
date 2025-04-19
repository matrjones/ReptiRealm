using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using ReptiRealm.Services;
using System.Security.Claims;

namespace ReptiRealm.Authentication
{
    public class SubscriptionAuthorizationFilter : IAuthorizationFilter
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly string _requiredPlan;

        public SubscriptionAuthorizationFilter(ISubscriptionService subscriptionService, string requiredPlan)
        {
            _subscriptionService = subscriptionService;
            _requiredPlan = requiredPlan;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            // Check if user is authenticated
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            // Get user's subscription
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new Microsoft.AspNetCore.Mvc.UnauthorizedResult();
                return;
            }

            var applicationUser = new ApplicationUser { Id = userId };
            var subscription = await _subscriptionService.GetSubscriptionByUserIdAsync(applicationUser);

            // Check if user has required subscription plan
            if (subscription == null || subscription.PlanName != _requiredPlan)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                return;
            }
        }
    }
} 