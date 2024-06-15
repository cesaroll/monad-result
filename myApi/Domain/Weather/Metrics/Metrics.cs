/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */

namespace myApi.Domain.Weather.Metrics;

public class Metrics : IMetrics
{
    public void LogMetric(string metricName, string metricValue)
    {
        Console.WriteLine($"Metric: {metricName} - Value: {metricValue}");
    }

    public async Task LogMetricAsync(string metricName, string metricValue)
    {
        await Task.Delay(500);
        LogMetric(metricName, metricValue);
    }
}
