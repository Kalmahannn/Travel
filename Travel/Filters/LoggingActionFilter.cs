using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TravelistaMVC.Filters
{
    public class LoggingActionFilter : IActionFilter
    {
        private readonly ILogger<LoggingActionFilter> _logger;

        public LoggingActionFilter(ILogger<LoggingActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation($"[Басталды] {context.ActionDescriptor.DisplayName}");

            if (context.Controller is Controller controller)
            {
                controller.TempData["ToastMessage"] = $"Бет ашылды: {context.ActionDescriptor.DisplayName}";
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation($"[Аяқталды] {context.ActionDescriptor.DisplayName}");
        }
    }
}
