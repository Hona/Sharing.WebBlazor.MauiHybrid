namespace MyShop.UI.Shared.Authentication;

public interface ICrossPlatformAuthenticationStateProvider
{
    Task LogOut();
    Task LogInAsync(string? returnUrl = null);
}