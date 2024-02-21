using System.Security.Claims;
using System.Threading.Tasks;
using Auth0.OidcClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MyShop.UI.Shared.Authentication;

namespace MyShop.UI.Mobile.Authentication;

public class Auth0AuthenticationStateProvider : AuthenticationStateProvider, ICrossPlatformAuthenticationStateProvider
{
    private ClaimsPrincipal currentUser = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly Auth0Client auth0Client;
    private readonly TokenAccessor _tokenAccessor;
    private readonly NavigationManager _navigationManager;
    
    public Auth0AuthenticationStateProvider(Auth0Client client, TokenAccessor tokenAccessor, NavigationManager navigationManager)
    {
        auth0Client = client;
        _tokenAccessor = tokenAccessor;
        _navigationManager = navigationManager;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync() =>
        Task.FromResult(new AuthenticationState(currentUser));

    public Task LogInAsync(string? returnUrl = null)
    {
        var loginTask = LogInAsyncCore();
        NotifyAuthenticationStateChanged(loginTask);

        return loginTask;

        async Task<AuthenticationState> LogInAsyncCore()
        {
            var user = await LoginWithAuth0Async();
            currentUser = user;
            
            return new AuthenticationState(currentUser);
        }
    }

    private async Task<ClaimsPrincipal> LoginWithAuth0Async()
    {
        var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity());
        
        // Return Url to "/"
        var loginResult = await auth0Client.LoginAsync(new { audience = "https://myshop.example.com"});

        if (!loginResult.IsError) {
            authenticatedUser = loginResult.User;
            
            _tokenAccessor.AccessToken = loginResult.AccessToken;
            // Todo: handle refreshing the token
        }

        return authenticatedUser;
    }

    public async Task LogOut()
    {
        await auth0Client.LogoutAsync();
        currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(
            Task.FromResult(new AuthenticationState(currentUser)));
    }
}