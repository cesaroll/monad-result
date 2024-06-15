/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using Fluent.Result;
using myApi.Domain.Weather.Requests;

namespace myApi;

public static class DayCountValidator
{
    private static readonly Error InvalidDayCountError = new(ErrorType.Validation, "Invalid day count");
    public static Result<WeatherForecastRequest> ValidateDayCount(this WeatherForecastRequest request) =>
        request.DayCount < 1 || request.DayCount > 7
            ? Result<WeatherForecastRequest>.Failure(InvalidDayCountError)
            : Result<WeatherForecastRequest>.Success(request);
}
