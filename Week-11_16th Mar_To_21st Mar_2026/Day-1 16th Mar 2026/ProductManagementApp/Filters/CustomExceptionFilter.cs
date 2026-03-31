using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ProductManagementApp.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var exceptionMessage = context.Exception.Message;
            var time = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");

            // Console log
            Console.WriteLine($"[ERROR] {exceptionMessage} | Time: {time}");

            var result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary(
                    metadataProvider: new EmptyModelMetadataProvider(),
                    modelState: context.ModelState)
            };

            // NOW SAFE ✅
            result.ViewData["ErrorMessage"] = "Something went wrong while loading products. Please try again.";

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}