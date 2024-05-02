using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text;
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

        public async Task<Task> Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();
            string requestBody = await new StreamReader(httpContext.Request.Body, Encoding.UTF8).ReadToEndAsync();
            var scheme = httpContext.Request.Scheme;
            var host = httpContext.Request.Host.ToString();
            var path = httpContext.Request.Path.ToString();
            var qs = httpContext.Request.QueryString.ToString();
            StringBuilder sb = new StringBuilder();
            sb.Append($"Path: {path} || Schema: {scheme} || Body: {requestBody} || Host: {host} || QueryString: {qs}");
            using (StreamWriter writeText = new StreamWriter("siu.txt"))
            {
                writeText.WriteLine(sb);
            }
            _logger.LogInformation(sb.ToString());
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
