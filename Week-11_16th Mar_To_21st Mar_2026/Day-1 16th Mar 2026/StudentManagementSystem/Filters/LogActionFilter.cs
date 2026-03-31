using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace StudentManagementSystem.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        // Fires BEFORE the action executes
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _logger.LogInformation($"[LOG] Action Starting: {actionName} at {timestamp}");
        }

        // Fires AFTER the action executes
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var actionName = context.ActionDescriptor.DisplayName;
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _logger.LogInformation($"[LOG] Action Completed: {actionName} at {timestamp}");
        }
    }
}