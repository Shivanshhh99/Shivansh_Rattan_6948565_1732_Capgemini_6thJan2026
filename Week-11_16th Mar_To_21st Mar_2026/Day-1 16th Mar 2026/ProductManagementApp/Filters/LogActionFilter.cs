using Microsoft.AspNetCore.Mvc.Filters;

namespace ProductManagementApp.Filters
{
    public class LogActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var time = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            Console.WriteLine($"[LOG - START] Action: {action} | Time: {time}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var action = context.ActionDescriptor.DisplayName;
            var time = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            Console.WriteLine($"[LOG - END] Action: {action} | Time: {time}");
        }
    }
}