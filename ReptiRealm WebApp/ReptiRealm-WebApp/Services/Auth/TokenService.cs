using ReptiRealm_WebApp.Services.Auth.Interfaces;

namespace ReptiRealm_WebApp.Services.Auth;

public class TokenService : ITokenService
{
    private string? _token;

    public Task<string?> GetTokenAsync()
        => Task.FromResult(_token);

    public Task SetTokenAsync(string token)
    {
        _token = token;
        return Task.CompletedTask;
    }
}