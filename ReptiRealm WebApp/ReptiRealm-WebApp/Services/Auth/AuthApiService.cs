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

    public async Task<bool> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("auth/login", new
        {
            email,
            password
        });

        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

        await _tokenService.SetTokenAsync(result!.Token);

        return true;
    }
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}