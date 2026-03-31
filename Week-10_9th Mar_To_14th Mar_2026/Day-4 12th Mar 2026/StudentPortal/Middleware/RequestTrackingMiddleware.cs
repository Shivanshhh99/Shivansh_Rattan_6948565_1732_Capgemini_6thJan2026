using System.Diagnostics;
using StudentPortal.Models;
using StudentPortal.Services;

namespace StudentPortal.Middleware
{
    public class RequestTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IRequestLogService logService)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            await _next(context);

            stopwatch.Stop();

            var log = new RequestLog
            {
                Url = context.Request.Path,
                ExecutionTime = stopwatch.ElapsedMilliseconds,
                RequestTime = DateTime.Now
            };

            logService.AddLog(log);
        }
    }
}