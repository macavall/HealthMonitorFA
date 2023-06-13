using Microsoft.Extensions.Logging;

public interface IPerfCounter
{
    public void GetPerformanceCounters(ILogger logger);
}