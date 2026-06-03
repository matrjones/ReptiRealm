using ReptiRealm_WebApp.Services.Auth.Interfaces;
using System.Net.Http.Headers;

namespace ReptiRealm_WebApp.Services.Auth;

public class AuthHeaderHandler : DelegatingHandler
{
    private readonly ITokenService _tokenService;

    public AuthHeaderHandler(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var token = await _tokenService.GetTokenAsync();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}