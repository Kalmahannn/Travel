using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TravelistaMVC.Middlewares
{
    public class ResponseTimeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseTimeMiddleware> _logger;

        public ResponseTimeMiddleware(RequestDelegate next, ILogger<ResponseTimeMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            // 1. Алдын-ала Header қосамыз
            context.Response.OnStarting(() =>
            {
                stopwatch.Stop();
                var elapsedMs = stopwatch.ElapsedMilliseconds;
                context.Response.Headers.Add("X-Response-Time-ms", elapsedMs.ToString());
                return Task.CompletedTask;
            });

            await _next(context); // Келесі Middleware-ге өткізу

            // 2. Логты жауап жіберілгеннен кейін жазамыз
            _logger.LogInformation($"[ResponseTime] {context.Request.Method} {context.Request.Path} сұранысы {stopwatch.ElapsedMilliseconds} мс ішінде орындалды.");
        }
    }
}
