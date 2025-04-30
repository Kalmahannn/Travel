using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TravelistaMVC.Filters
{
    public class CustomResultFilter : IResultFilter
    {
        private readonly ILogger<CustomResultFilter> _logger;

        public CustomResultFilter(ILogger<CustomResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            // View қайтарылар алдында орындалады
            _logger.LogInformation($"[ResultExecuting] Нәтиже дайындалып жатыр: {context.Result}");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // View қайтарылғаннан кейін орындалады
            _logger.LogInformation($"[ResultExecuted] Нәтиже клиентке жіберілді: {context.Result}");
        }
    }
}
