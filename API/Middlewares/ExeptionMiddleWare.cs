using System.Net;
using System.Text.Json;
using Application.Core;

namespace API.Middlewares
{
    public class ExeptionMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddleWare> _logger;
        private readonly IHostEnvironment _env;
        public ExeptionMiddleWare(RequestDelegate next, ILogger<ExeptionMiddleWare> logger,
        IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;

        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new AppExeption(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new AppExeption(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);

            }
        }
    }
}