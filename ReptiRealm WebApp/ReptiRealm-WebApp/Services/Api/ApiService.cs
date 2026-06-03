namespace ReptiRealm_WebApp.Services.Api;

public abstract class ApiService
{
    protected readonly HttpClient _http;

    protected ApiService(HttpClient http)
    {
        _http = http;
    }

    protected async Task<T?> GetAsync<T>(string url)
        => await _http.GetFromJsonAsync<T>(url);

    protected async Task<T?> PostAsync<T>(string url, object data)
    {
        var response = await _http.PostAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> PutAsync<T>(string url, object data)
    {
        var response = await _http.PutAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> PatchAsync<T>(string url, object? data = null)
    {
        var response = await _http.PatchAsJsonAsync(url, data);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<T>();
    }

    protected async Task<T?> DeleteAsync<T>(string url)
    {
        var response = await _http.DeleteAsync(url);
        response.EnsureSuccessStatusCode();

        if (response.Content.Headers.ContentLength == 0)
            return default;

        return await response.Content.ReadFromJsonAsync<T>();
    }
}