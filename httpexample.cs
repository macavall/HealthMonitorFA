using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace HealthMonitorFA56
{
    public class httpexample
    {
        private readonly ILogger _logger;
        private readonly IPerfCounter perfCounters;

        public httpexample(ILoggerFactory loggerFactory, IPerfCounter _perfCounters)
        {
            _logger = loggerFactory.CreateLogger<httpexample>();
            perfCounters = _perfCounters;
        }

        [Function("httpexample")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            //List<Task> tasks = new List<Task>();

            perfCounters.GetPerformanceCounters(_logger);

            // Create 500 threads all awaiting for one minute
            //Thread.Sleep(60000);

            for (int x = 0; x < 100; x++)
            {
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(60000);
                });
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
