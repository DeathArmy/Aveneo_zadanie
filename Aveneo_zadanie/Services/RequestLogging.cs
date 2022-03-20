using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Aveneo.Services
{
    public class RequestLogging
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public RequestLogging(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogging>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "{host} - {url} {method} {statusCode}",                  
                    context.Request?.Host,
                    context.Request?.Path.Value,
                    context.Request?.Method,
                    context.Response?.StatusCode);
            }
        }
    }
}
