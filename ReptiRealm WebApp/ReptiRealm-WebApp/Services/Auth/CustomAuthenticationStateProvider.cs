using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

namespace ReptiRealm_WebApp.Services.Auth;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenService _tokenService;

    public CustomAuthenticationStateProvider(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        GetAuthenticationStateFromStorageAsync();

    public Task RefreshAuthenticationStateAsync()
    {
        var task = GetAuthenticationStateFromStorageAsync();
        NotifyAuthenticationStateChanged(task);
        return task;
    }

    private async Task<AuthenticationState> GetAuthenticationStateFromStorageAsync()
    {
        var token = await _tokenService.GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            return Anonymous();
        }

        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            if (jwt.ValidTo < DateTime.UtcNow)
            {
                await _tokenService.RemoveTokenAsync();
                return Anonymous();
            }

            return new AuthenticationState(
                new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "jwt")));
        }
        catch
        {
            await _tokenService.RemoveTokenAsync();
            return Anonymous();
        }
    }

    public void NotifyUserAuthentication(string token)
    {
        _tokenService.SetTokenAsync(token); // fire-and-forget is fine here

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(token);

        var identity = new ClaimsIdentity(jwt.Claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(user)));
    }

    public void NotifyUserLogout()
    {
        _tokenService.RemoveTokenAsync();

        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
    }

    private static AuthenticationState Anonymous()
        => new(new ClaimsPrincipal(new ClaimsIdentity()));
}