/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Fluent.Result;
using myApi.Domain.Shared.Dtos;

namespace myApi.Domain.Weather.Persist;

public interface IWeatherRepo
{
    public Task<Result<WeatherForecast>> SaveWeatherForecastAsync(WeatherForecast forecast);
}
