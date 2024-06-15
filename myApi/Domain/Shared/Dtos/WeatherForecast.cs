/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

namespace myApi.Domain.Shared.Dtos;

public record WeatherForecast(City City, List<WeatherDetail> Details);
