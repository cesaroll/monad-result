/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Fluent.Result;
using myApi.Domain.Weather.Requests;

namespace myApi;

public static class CityValidator
{
    private static readonly Error InvalidCityError = new(ErrorType.Validation, "Invalid city name");

    public static Result<WeatherForecastRequest> ValidateCity(this WeatherForecastRequest request) =>
        string.IsNullOrWhiteSpace(request.City.Name)
            ? Result<WeatherForecastRequest>.Failure(InvalidCityError)
            : Result<WeatherForecastRequest>.Success(request);
}
