/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
using myApi.Domain.Shared.Dtos;

namespace myApi.Domain.Weather.Requests;

public record class WeatherForecastRequest(City City, int DayCount = 5, CancellationToken ct = default);
