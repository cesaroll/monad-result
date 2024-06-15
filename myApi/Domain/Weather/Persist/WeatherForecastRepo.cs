/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Fluent.Result;
using myApi.Domain.Shared.Dtos;

namespace myApi.Domain.Weather.Persist;

public class WeatherForecastRepo : IWeatherRepo
{
    public async Task<Result<WeatherForecast>> SaveWeatherForecastAsync(WeatherForecast forecast)
    {
        await Task.Delay(500);
        Console.WriteLine($"Saving weather forecast for {forecast.City.Name}");

        try
        {
            // Save weather forecast
            return Result<WeatherForecast>.Success(forecast);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error persisting weather forecast for {forecast.City.Name}: {ex.Message}");
            return Result<WeatherForecast>.Failure(new(ErrorType.Failure, "Error persisting weather forecast"));
        }
    }
}
