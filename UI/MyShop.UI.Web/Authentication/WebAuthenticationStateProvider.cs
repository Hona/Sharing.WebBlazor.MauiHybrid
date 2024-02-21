using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using MyShop.UI.Shared.Authentication;

namespace MyShop.UI.Web.Authentication;

public class WebAuthenticationStateProvider(NavigationManager navigationManager)
    : ICrossPlatformAuthenticationStateProvider
{
    public Task LogOut()
    {
        navigationManager.NavigateToLogout("authentication/logout");
        return Task.CompletedTask;
    }

    public Task LogInAsync(string? returnUrl = null)
    {
        const string loginRoute = "authentication/login";

        navigationManager.NavigateToLogin(loginRoute, new InteractiveRequestOptions
        {
            ReturnUrl = returnUrl ?? "/", // Don't return to the history entry which will be /login
            Interaction = InteractionType.SignIn
        });

        
        return Task.CompletedTask;
    }
}