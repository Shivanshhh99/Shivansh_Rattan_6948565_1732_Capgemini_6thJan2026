using System.Security.Claims;

namespace SecureJwtLoggingApi.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.Request.Path;
            var method = context.Request.Method;
            var timestamp = DateTime.Now;

            var userId = context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Anonymous";
            var username = context.User?.Identity?.Name ?? "Anonymous";

            _logger.LogInformation(
                "Request received | Method: {Method} | Endpoint: {Endpoint} | Time: {Timestamp} | UserId: {UserId} | Username: {Username}",
                method, endpoint, timestamp, userId, username);

            await _next(context);

            if (context.Response.StatusCode == 401)
            {
                _logger.LogWarning("Unauthorized access to {Endpoint} | Method: {Method}", endpoint, method);
            }

            if (context.Response.StatusCode == 403)
            {
                _logger.LogWarning("Forbidden access to {Endpoint} | Method: {Method}", endpoint, method);
            }
        }
    }
}