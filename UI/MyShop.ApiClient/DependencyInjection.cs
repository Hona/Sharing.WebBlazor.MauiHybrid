using Microsoft.Extensions.DependencyInjection;

namespace MyShop.ApiClient;

public static class DependencyInjection
{
    public static void AddMyShopApiClients(this IServiceCollection services, Action<IHttpClientBuilder> configureBuilder, string baseAddress = "https://localhost:7023/")
    {
        var clientBuilder = services.AddHttpClient<WeatherForecastClient>(client =>
        {
            // TODO: Move to config!
            client.BaseAddress = new Uri(baseAddress);
        });
        
        configureBuilder(clientBuilder);
    }
}