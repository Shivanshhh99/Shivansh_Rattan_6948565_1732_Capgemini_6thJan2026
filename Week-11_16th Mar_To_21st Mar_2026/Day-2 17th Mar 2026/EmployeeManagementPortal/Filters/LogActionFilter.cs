using Microsoft.AspNetCore.Mvc.Filters;

namespace EmployeeManagementPortal.Filters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var user = context.HttpContext.Session.GetString("LoggedInUser") ?? "Anonymous";
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _logger.LogInformation(
                "[LOG] ▶ Action Starting | Action: {Action} | User: {User} | Time: {Time}",
                action, user, timestamp);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var user = context.HttpContext.Session.GetString("LoggedInUser") ?? "Anonymous";
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            _logger.LogInformation(
                "[LOG] ✔ Action Completed | Action: {Action} | User: {User} | Time: {Time}",
                action, user, timestamp);
        }
    }
}