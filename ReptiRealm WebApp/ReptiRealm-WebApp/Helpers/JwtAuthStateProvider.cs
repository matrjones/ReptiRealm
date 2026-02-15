using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace ReptiRealm_WebApp.Helpers
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _storage;

        private const string TokenKey = "AuthToken";
        private const string UsernameKey = "Username";

        private string? _token;
        private string? _username;
        private ClaimsPrincipal? _cachedUser;

        public JwtAuthStateProvider(ProtectedSessionStorage storage)
        {
            _storage = storage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // Return cached if available
            if (!string.IsNullOrEmpty(_token) && _cachedUser != null)
                return new AuthenticationState(_cachedUser);

            // Default anonymous while JS interop may not be ready
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());

            try
            {
                var tokenResult = await _storage.GetAsync<string>(TokenKey);
                if (tokenResult.Success && !string.IsNullOrWhiteSpace(tokenResult.Value))
                {
                    _token = tokenResult.Value;

                    var usernameResult = await _storage.GetAsync<string>(UsernameKey);
                    _username = usernameResult.Success && !string.IsNullOrWhiteSpace(usernameResult.Value)
                        ? usernameResult.Value
                        : "User";

                    _cachedUser = CreateClaimsPrincipal(_username);
                    return new AuthenticationState(_cachedUser);
                }
            }
            catch (InvalidOperationException)
            {
                // JS not ready → return anonymous for now
                return new AuthenticationState(anonymous);
            }

            return new AuthenticationState(anonymous);
        }

        public async Task SetTokenAsync(string token, string username)
        {
            _token = token;
            _username = username;
            _cachedUser = CreateClaimsPrincipal(username);

            // Save to session storage
            await _storage.SetAsync(TokenKey, token);
            await _storage.SetAsync(UsernameKey, username);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task ClearTokenAsync()
        {
            _token = null;
            _username = null;
            _cachedUser = null;

            await _storage.DeleteAsync(TokenKey);
            await _storage.DeleteAsync(UsernameKey);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string?> GetTokenAsync() =>
            (await GetAuthenticationStateAsync()).User.Identity?.IsAuthenticated == true
                ? _token
                : null;

        private ClaimsPrincipal CreateClaimsPrincipal(string username)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username)
            }, "Jwt");

            return new ClaimsPrincipal(identity);
        }
    }
}
