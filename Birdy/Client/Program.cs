global using Birdy.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using Birdy.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Security.Claims;
using Birdy.Client.Infrastructure;
using Birdy.Shared.Enums;
using Microsoft.AspNetCore.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

/*Аутентификация/Авторизация*/
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
builder.Services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<CartService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorageService;

    public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        AuthenticationState CreateAnonymous()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, UserRole.Anonymous)
            };

            var anonymousIdentity = new ClaimsIdentity(claims);
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            return new AuthenticationState(anonymousPrincipal);
        }

        var token = await _localStorageService.GetAsync<Token>(nameof(Token));

        if (token is null)
        {
            return CreateAnonymous();
        }

        if (token.ExpiredAt < DateTime.Now)
        {
            return CreateAnonymous();
        }

        var claims = new List<Claim> {
            new Claim(ClaimTypes.Role, token.Role),
            new Claim(ClaimTypes.Expired, token.ExpiredAt.ToLongDateString())
        };

        var identity = new ClaimsIdentity(claims, "Token");
        var principal = new ClaimsPrincipal(identity);
        return new AuthenticationState(principal);
    }

    public void MakeUserAnonymous()
    {
        _localStorageService.RemoveAsync(nameof(Token));

        var anonymousIdentity = new ClaimsIdentity();
        var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
        var authState = Task.FromResult(new AuthenticationState(anonymousPrincipal));
        NotifyAuthenticationStateChanged(authState);
    }
}