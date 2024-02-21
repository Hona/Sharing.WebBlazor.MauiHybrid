using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace MyShop.UI.Shared;

public static class DependencyInjection
{
    public static void AddMyShopUI(this IServiceCollection services)
    {
        services.AddMudServices();
    }
}