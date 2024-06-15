/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Fluent.Result;
using myApi.Domain.Shared.Dtos;
using myApi.Domain.Weather.Requests;

namespace myApi.Domain.Weather;

public class WeatherService : IWeatherService
{
    private readonly Random _random;

    private readonly string[] summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Planchando el Diablo"
    ];

    public WeatherService(Random random)
    {
        _random = random;
    }

    public Result<WeatherForecast> GetWeatherForecast(WeatherForecastRequest request)
    {
        Console.WriteLine($"Getting weather forecast for {request.City.Name} for {request.DayCount} days");

        try {
            if(_random.Next(0, 10) == 0)
                throw new Exception("Error getting weather forecast"); // Simulate error

            var details =  Enumerable.Range(1, request.DayCount).Select(index =>
            new WeatherDetail
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToList();

            return Result<WeatherForecast>.Success(new WeatherForecast(request.City, details));
        } catch(Exception ex) {
            Console.WriteLine($"Error getting weather forecast for {request.City.Name}: {ex.Message}");
            return Result<WeatherForecast>.Failure(new(ErrorType.Failure, "Error retrieving weather forecast. Please try again later."));
        }

    }

    public async Task<Result<WeatherForecast>> GetWeatherForecastAsync(WeatherForecastRequest request)
    {
        await Task.Delay(500);

        return GetWeatherForecast(request);
    }
}
