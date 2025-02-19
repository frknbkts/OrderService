using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using OrderService.Domain.Exceptions;

namespace OrderService.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
                _logger.LogError(ex, "An error occurred while processing the request");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var (statusCode, error) = exception switch
            {
                OrderNotFoundException ex => (HttpStatusCode.NotFound, new { error = ex.Message }),
                InvalidOrderStateException ex => (HttpStatusCode.BadRequest, new { error = ex.Message }),
                OrderDomainException ex => (HttpStatusCode.BadRequest, new { error = ex.Message }),
                _ => (HttpStatusCode.InternalServerError, new { error = "An unexpected error occurred" })
            };

            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsJsonAsync(error);
        }
    }
} 