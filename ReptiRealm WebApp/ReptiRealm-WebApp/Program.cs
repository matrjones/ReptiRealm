using Microsoft.Extensions.Options;
using ReptiRealm_WebApp;
using ReptiRealm_WebApp.Services.Api;
using ReptiRealm_WebApp.Services.Api.Interfaces;
using ReptiRealm_WebApp.Services.Auth;
using ReptiRealm_WebApp.Services.Auth.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddHttpClient<IReptileApiService, ReptileApiService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddHttpClient<AuthApiService>((sp, client) =>
{
    var settings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    client.BaseAddress = new Uri(settings.BaseUrl);
});

builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddTransient<AuthHeaderHandler>();

builder.Services.AddHttpClient<AuthApiService>();
builder.Services.AddHttpClient<IReptileApiService, ReptileApiService>()
    .AddHttpMessageHandler<AuthHeaderHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
