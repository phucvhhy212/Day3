using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Day3
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            httpContext.Items.Add("Schema", httpContext.Request.Scheme);
            httpContext.Items.Add("Host", httpContext.Request.Host.ToString());
            httpContext.Items.Add("Path", httpContext.Request.Path.ToString());
            httpContext.Items.Add("QueryString", httpContext.Request.QueryString.ToString());
            httpContext.Items.Add("Body", httpContext.Request.Body.ToString());
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
