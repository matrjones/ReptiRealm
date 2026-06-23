using ReptiRealm_WebApp.Services.Auth.Interfaces;

namespace ReptiRealm_WebApp.Services.Auth;

public class AuthApiService
{
    private readonly HttpClient _http;
    private readonly ITokenService _tokenService;

    public AuthApiService(HttpClient http, ITokenService tokenService)
    {
        _http = http;
        _tokenService = tokenService;
    }

    public async Task<LoginResponse?> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new
        {
            email,
            password
        });

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        if (result?.Token is null)
            return null;

        await _tokenService.SetTokenAsync(result.Token);

        return result;
    }

    public async Task Logout()
    {
        await _tokenService.RemoveTokenAsync();
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}