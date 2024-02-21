using System.Net.Http.Json;
using MyShop.Shared.Entities;

namespace MyShop.ApiClient;

public class WeatherForecastClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetForecastAsync()
    {
        var response = await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast");

        return response ?? Array.Empty<WeatherForecast>();
    }
}