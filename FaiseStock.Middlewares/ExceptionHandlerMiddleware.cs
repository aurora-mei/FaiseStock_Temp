using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FaiseStock.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred.");

                var problemDetails = new
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    ErrorMesage = ex.Message,
                    Data = $"Cannot {context.Request.Method} {context.Request.Path}"
                };

                context.Response.StatusCode = problemDetails.StatusCode;
                context.Response.ContentType = "application/json";

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var jsonResponse = JsonSerializer.Serialize(problemDetails, options);

                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
