using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

namespace ReptiRealm_WebApp.Services.Auth;

public class TokenService : ITokenService
{
    private const string _storageKey = "auth_session";
    private readonly ProtectedLocalStorage _localStorage;

    public TokenService(ProtectedLocalStorage localStorage)
    {
        _localStorage = localStorage;
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var result = await _localStorage.GetAsync<string>(_storageKey);
            return result.Success ? result.Value : null;
        }
        catch (InvalidOperationException ex) when (IsJsInteropUnavailable(ex))
        {
            return null;
        }
    }

    public async Task SetTokenAsync(string token)
    {
        try
        {
            await _localStorage.SetAsync(_storageKey, token);
        }
        catch (InvalidOperationException ex) when (IsJsInteropUnavailable(ex))
        {
        }
    }

    public async Task RemoveTokenAsync()
    {
        try
        {
            await _localStorage.DeleteAsync(_storageKey);
        }
        catch (InvalidOperationException ex) when (IsJsInteropUnavailable(ex))
        {
        }
    }

    private static bool IsJsInteropUnavailable(InvalidOperationException ex) =>
        ex.Message.Contains("JavaScript interop", StringComparison.OrdinalIgnoreCase)
        || ex.Message.Contains("statically rendered", StringComparison.OrdinalIgnoreCase);
}