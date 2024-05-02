using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Day3
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomMiddleware> _logger;

        public CustomMiddleware(RequestDelegate next, ILogger<CustomMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var scheme = httpContext.Request.Scheme;
            var host = httpContext.Request.Host.ToString();
            var path = httpContext.Request.Path.ToString();
            var qs = httpContext.Request.QueryString.ToString();
            var body = httpContext.Request.Body.ToString();

            using (StreamWriter writeText = new StreamWriter("siu.txt"))
            {
                writeText.WriteLine($"Path: {path}");
                writeText.WriteLine($"Schema: {scheme}");
                writeText.WriteLine($"Body: {body}");
                writeText.WriteLine($"Host: {host}");
                writeText.WriteLine($"QueryString: {qs}");
            }
            _logger.LogInformation($"Path: {path}");
            _logger.LogInformation($"Body: {body}");
            _logger.LogInformation($"Schema: {scheme}");
            _logger.LogInformation($"QueryString: {qs}");
            _logger.LogInformation($"Host: {host}");
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class CustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomMiddleware>();
        }
    }
}
