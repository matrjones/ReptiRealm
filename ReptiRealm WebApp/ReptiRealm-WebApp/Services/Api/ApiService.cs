using System.Net.Http.Headers;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

public abstract class ApiService
{
    protected readonly HttpClient _http;
    private readonly ITokenService _tokenService;

    protected ApiService(HttpClient http, ITokenService tokenService)
    {
        _http = http;
        _tokenService = tokenService;
    }

    private async Task AttachAuthHeader()
    {
        var token = await _tokenService.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
        else
        {
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }

    protected async Task<T?> GetAsync<T>(string url)
    {
        await AttachAuthHeader();
        return await _http.GetFromJsonAsync<T>(url);
    }

    protected async Task<T?> PostAsync<T>(string url, object data)
    {
        await AttachAuthHeader();

        var response = await _http.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> PutAsync<T>(string url, object data)
    {
        await AttachAuthHeader();

        var response = await _http.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> PatchAsync<T>(string url, object? data = null)
    {
        await AttachAuthHeader();

        var response = await _http.PatchAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> DeleteAsync<T>(string url)
    {
        await AttachAuthHeader();

        var response = await _http.DeleteAsync(url);
        response.EnsureSuccessStatusCode();

        if (response.Content.Headers.ContentLength == 0)
            return default;

        return await response.Content.ReadFromJsonAsync<T>();
    }
}