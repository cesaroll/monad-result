/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace myApi.Domain.Shared.Dtos;

public record WeatherDetail(DateOnly Date, int TemperatureC, string Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
