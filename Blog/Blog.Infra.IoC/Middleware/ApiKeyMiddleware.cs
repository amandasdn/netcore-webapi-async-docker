using Blog.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Blog.Infra.IoC.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context)
        {
            var key = _configuration["ApiKey:KeyName"];
            var value = _configuration["ApiKey:KeyValue"];

            if (string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(value))
            {
                await UnauthorizeResponse(context, 99, "API key not found on settings.");
                return;
            }

            var headerValue = context.Request.Headers[key];
            if (string.IsNullOrWhiteSpace(headerValue))
            {
                await UnauthorizeResponse(context, 1, "API key not found.");
                return;
            }

            if (!string.Equals(value, headerValue, StringComparison.InvariantCultureIgnoreCase))
            {
                await UnauthorizeResponse(context, 2, "Invalid API key.");
                return;
            }

            await _next.Invoke(context);
        }

        private static async Task UnauthorizeResponse(HttpContext context, int code, string message)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.Response.ContentType = "application/json";

            var response = new Result();
            response.AddErrorMessage($"({code}) {message}");

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
