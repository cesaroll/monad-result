using myApi.Domain.Weather.Persist;
using myApi.Domain.Shared.Dtos;
using myApi.Domain.Weather;
using myApi.Domain.Weather.Requests;
using myApi.Domain.Weather.Publish;
using myApi;
using Fluent.Result;
using myApi.Domain.Weather.Metrics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IWeatherService, WeatherService>();
builder.Services.AddSingleton<IWeatherRepo, WeatherForecastRepo>();
builder.Services.AddSingleton<IWeatherPub, WeatherPub>();
builder.Services.AddSingleton<IMetrics, Metrics>();
builder.Services.AddSingleton<Random>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/ping",() => "pong")
.WithName("Ping")
.WithOpenApi();

app.MapGet("/api/forecast/normal/{CityName}/{DayCount}",
    async
    (string CityName, int DayCount,
    IWeatherService weatherService,
    IMetrics metrics,
    IWeatherRepo weatherRepo,
    IWeatherPub weatherPub,
    CancellationToken ct) =>
{
    var request = new WeatherForecastRequest(new City(CityName), DayCount, ct);

    // Validate
    request.ValidateCity();
    request.ValidateDayCount();

    // Retrieve
    var result = await weatherService.GetWeatherForecastAsync(request);
    if(!result.IsSuccess)
        return Results.BadRequest(result.Error.Description);

    var forecast = result.Value;

    // Log Metrics
    await metrics.LogMetricAsync("City", forecast.City.Name);

    // Persist
    await weatherRepo.SaveWeatherForecastAsync(forecast);

    // Publish
    await weatherPub.PublishWeatherForecastAsync(forecast);

    return Results.Ok(forecast);

})
.WithName("GetWeatherForecastAsync")
.WithOpenApi();

app.MapGet("/api/forecast/monad/{CityName}/{DayCount}",
    (string CityName, int DayCount,
    IWeatherService weatherService,
    IMetrics metrics,
    IWeatherRepo weatherRepo,
    IWeatherPub weatherPub,
    CancellationToken ct) =>
{
    var request = new WeatherForecastRequest(new City(CityName), DayCount, ct);

    return request
        .ValidateCity()
        .Bind(req => req.ValidateDayCount())
        .Bind(req => weatherService.GetWeatherForecast(req))
        .Tap(forecast => metrics.LogMetric("City", forecast.City.Name))
        .Match(
            success => Results.Ok(success),
            failure => Results.BadRequest(failure.Description)
        );
})
.WithName("GetWeatherForecastMonadSync")
.WithOpenApi();

app.MapGet("/api/forecast/{CityName}/{DayCount}",
    async
    (string CityName, int DayCount,
    IWeatherService weatherService,
    IMetrics metrics,
    IWeatherRepo weatherRepo,
    IWeatherPub weatherPub,
    CancellationToken ct) =>
{
    var request = new WeatherForecastRequest(new City(CityName), DayCount, ct);

    return await request
    .ValidateCity()
    .Bind(req => req.ValidateDayCount())
    .Bind(req => weatherService.GetWeatherForecastAsync(req))
    .Bind(forecast => weatherRepo.SaveWeatherForecastAsync(forecast))
    .Bind(forecast => weatherPub.PublishWeatherForecastAsync(forecast))
    .Tap(forecast => metrics.LogMetricAsync("City", forecast.City.Name))
    .Match(
        success => Results.Ok(success),
        failure => Results.BadRequest(failure.Description)
    );
})
.WithName("GetWeatherForecastMonadAsync")
.WithOpenApi();

app.Run();

