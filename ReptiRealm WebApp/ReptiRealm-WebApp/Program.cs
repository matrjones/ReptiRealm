using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;
using ReptiRealm_WebApp;
using ReptiRealm_WebApp.Services.Api;
using ReptiRealm_WebApp.Services.Api.Interfaces;
using ReptiRealm_WebApp.Services.Auth;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------
// UI
// ------------------------------------
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ------------------------------------
// CONFIG
// ------------------------------------
builder.Services.Configure<ApiSettings>(
    builder.Configuration.GetSection("ApiSettings"));

// ------------------------------------
// AUTH (JWT STATE ONLY)
// ------------------------------------
builder.Services.AddAuthorization();
builder.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, BlazorAuthorizationMiddlewareResultHandler>();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// ------------------------------------
// LOCAL STORAGE AUTH
// ------------------------------------
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ITokenService, TokenService>();

// ------------------------------------
// HTTP CLIENTS
// ------------------------------------
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddHttpClient<IReptileApiService, ReptileApiService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
})
.AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddHttpClient<AuthApiService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

// ------------------------------------
// BUILD
// ------------------------------------
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();