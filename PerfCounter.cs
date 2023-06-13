using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;

public class PerfCounter : IPerfCounter
{
    public void GetPerformanceCounters(ILogger logger)
    {
        string? perfResult = Environment.GetEnvironmentVariable("WEBSITE_COUNTERS_ALL");

        // If perfResult is NULL then set string "No performance counters found"
        string finalResult = perfResult ?? "No performance counters found";


        // Get the number of threads from dynamic object
        //
        if (finalResult != "No performance counters found")
        {
            var jsonDoc = JsonDocument.Parse(finalResult);
            var root = jsonDoc.RootElement;
            var threads = root.GetProperty("app").GetProperty("threads") ;

            logger.LogInformation(threads.ToString());
        }
        else
        {
            logger.LogInformation(finalResult);
        }
    }
}