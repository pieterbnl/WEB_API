using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace MonitoringApi.HealthChecks;

public class RandomHealthCheck : IHealthCheck
{
    // Async call to check the health of the application
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        // Health check example: check simulated response time
        int responseTimeInMs = Random.Shared.Next(300);
        
        if (responseTimeInMs < 100)
        {
            return Task.FromResult(HealthCheckResult.Healthy(
                $"Response time is {responseTimeInMs} ms"));
        }
        else if (responseTimeInMs <200)
        {
            return Task.FromResult(HealthCheckResult.Degraded(
                $"Response time is {responseTimeInMs} ms"));
        }
        else
        {
            return Task.FromResult(HealthCheckResult.Unhealthy(
                $"Response time is {responseTimeInMs} ms"));
        }
    }
}