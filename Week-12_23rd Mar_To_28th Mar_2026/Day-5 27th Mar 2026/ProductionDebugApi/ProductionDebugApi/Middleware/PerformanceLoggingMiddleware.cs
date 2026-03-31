using System.Diagnostics;

namespace ProductionDebuggingApi.Middleware
{
    public class PerformanceLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<PerformanceLoggingMiddleware> _logger;

        public PerformanceLoggingMiddleware(RequestDelegate next, ILogger<PerformanceLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var apiPath = context.Request.Path;

            _logger.LogInformation("API Started: {ApiPath} at {StartTime}", apiPath, DateTime.Now);

            try
            {
                await _next(context);

                stopwatch.Stop();

                _logger.LogInformation("API Ended: {ApiPath} at {EndTime}", apiPath, DateTime.Now);
                _logger.LogInformation("Execution Duration for {ApiPath}: {Duration} ms", apiPath, stopwatch.ElapsedMilliseconds);

                if (stopwatch.Elapsed.TotalSeconds > 3)
                {
                    _logger.LogWarning("Slow API detected: {ApiPath} took {Duration} sec", apiPath, stopwatch.Elapsed.TotalSeconds);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(ex, "API failed: {ApiPath} after {Duration} ms", apiPath, stopwatch.ElapsedMilliseconds);

                throw;
            }
        }
    }
}