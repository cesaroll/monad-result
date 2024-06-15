/*
 * @author: Cesar Lopez
 * @copyright 2024 - All rights reserved
 */
namespace myApi.Domain.Weather.Metrics;

public interface IMetrics
{
    public void LogMetric(string metricName, string metricValue);
    public Task LogMetricAsync(string metricName, string metricValue);
}
