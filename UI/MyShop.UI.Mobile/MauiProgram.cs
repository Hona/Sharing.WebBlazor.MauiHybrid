using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using MyShop.ApiClient;
using MyShop.Shared.RBAC;
using MyShop.UI.Mobile.Authentication;
using MyShop.UI.Shared;
using MyShop.UI.Shared.Authentication;
using Auth0Client = Auth0.OidcClient.Auth0Client;

namespace MyShop.UI.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        
        builder.Services.AddMyShopUI();

        builder.Services.AddSingleton<TokenAccessor>();
        builder.Services.AddSingleton<TokenDelegatingHandler>();
        
        // Required for the Android emulator bridge
        if (DeviceInfo.Platform == DevicePlatform.Android)
        {
            builder.Services.AddMyShopApiClients(x =>
            {
                x.AddHttpMessageHandler<TokenDelegatingHandler>();
            }, "http://10.0.2.2:5131");    
        }
        else
        {
            builder.Services.AddMyShopApiClients(x =>
            {
                x.AddHttpMessageHandler<TokenDelegatingHandler>();
            });    
        }
    
        // TODO: Refactor to configuration
        builder.Services.AddSingleton(new Auth0Client(new()
        {
            ClientId = "rL3wxDLETpF7vCvhWiPkHDnemIH0pVDu",
            Domain = "myshop.au.auth0.com",
            Scope = "openid profile",
            RedirectUri = "myshop://callback",
            PostLogoutRedirectUri = "myshop://callback"
        }));
        builder.Services.AddAuthorizationCore(options =>
        {
            options.AddUserRolePolicies();
        });

        builder.Services.AddSingleton<Auth0AuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider, Auth0AuthenticationStateProvider>(sp => sp.GetRequiredService<Auth0AuthenticationStateProvider>());
        builder.Services.AddScoped<ICrossPlatformAuthenticationStateProvider>(sp => sp.GetRequiredService<Auth0AuthenticationStateProvider>());

        return builder.Build();
    }
}