/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

using Fluent.Result;
using myApi.Domain.Shared.Dtos;
using myApi.Domain.Weather.Requests;

namespace myApi.Domain.Weather;

public interface IWeatherService
{
    public Result<WeatherForecast> GetWeatherForecast(WeatherForecastRequest request);
    public Task<Result<WeatherForecast>> GetWeatherForecastAsync(WeatherForecastRequest request);
}
