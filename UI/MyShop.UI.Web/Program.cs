using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyShop.ApiClient;
using MyShop.Shared.RBAC;
using MyShop.UI.Shared;
using MyShop.UI.Shared.Authentication;
using MyShop.UI.Web;
using MyShop.UI.Web.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<WebApp>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMyShopUI();
builder.Services.AddScoped<ApiAuthorizationMessageHandler>();
builder.Services.AddMyShopApiClients(x =>
{
    x.AddHttpMessageHandler<ApiAuthorizationMessageHandler>();
});

builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});

builder.Services.AddAuthorizationCore(options =>
{
    options.AddUserRolePolicies();
});

builder.Services.AddScoped<ICrossPlatformAuthenticationStateProvider, WebAuthenticationStateProvider>();

await builder.Build().RunAsync();