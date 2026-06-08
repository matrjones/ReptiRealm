using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace ReptiRealm_WebApp.Services.Auth;

/// <summary>
/// Lets Blazor's AuthorizeRouteView handle unauthenticated users instead of the
/// HTTP pipeline issuing a challenge (which requires a registered auth scheme).
/// </summary>
public sealed class BlazorAuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        return next(context);
    }
}
