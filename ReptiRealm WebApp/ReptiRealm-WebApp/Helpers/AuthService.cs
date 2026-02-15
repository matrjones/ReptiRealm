using ReptiRealm_WebApp.DTOs.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;

namespace ReptiRealm_WebApp.Helpers
{
    public class AuthService
    {
        private readonly HttpClient _http;
        private readonly JwtAuthStateProvider _authProvider;

        private const string LoginUrl = "https://localhost:7264/api/Auth/login";

        public AuthService(HttpClient http, JwtAuthStateProvider authProvider)
        {
            _http = http;
            _authProvider = authProvider;
        }

        public async Task<bool> LoginAsync(LoginDto model)
        {
            var response = await _http.PostAsJsonAsync(LoginUrl, model);
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<LoginResult>();
            if (result is null) return false;

            await _authProvider.SetTokenAsync(result.Token, result.Username);
            return true;
        }

        public async Task LogoutAsync() => await _authProvider.ClearTokenAsync();

        public async Task<string?> GetTokenAsync() => await _authProvider.GetTokenAsync();

        private async Task<HttpRequestMessage> CreateRequestAsync(HttpMethod method, string url)
        {
            var token = await GetTokenAsync();
            var req = new HttpRequestMessage(method, url);
            if (!string.IsNullOrEmpty(token))
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return req;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            var request = await CreateRequestAsync(HttpMethod.Get, url);
            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest payload)
        {
            var request = await CreateRequestAsync(HttpMethod.Post, url);
            request.Content = JsonContent.Create(payload);

            var response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<TResponse>();
        }

        public record LoginResult(string Token, string Username);
    }
}
