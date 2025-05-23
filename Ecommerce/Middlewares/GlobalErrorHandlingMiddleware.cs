using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ecommerce.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Logic
            // Call the next middlware
            try
            {
                await _next(context);

                // Handle Not found Case for the request path "url" as it doesn't throw excpetions
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundAsync(context);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong while calling endpoint : {context.Request.Path} with exception {ex.Message}");

                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleNotFoundAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var response = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Message = "EndPoint Not Found",
                Details = $"EndPoint {context.Request.Path} Not Found"
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                _ => (int)HttpStatusCode.InternalServerError
            };

            context.Response.ContentType = "application/json";

            var error = ex switch
            {
                NotFoundException => new ErrorDetails()
                {
                    StatusCode  = (int)HttpStatusCode.NotFound,
                    Details     = ex.Message,
                    Message     = "Not Found"
                },
                UnAuthorizedException => new ErrorDetails()
                {
                    StatusCode  = context.Response.StatusCode,
                    Details     = ex.Message,
                    Message     = "UnAuthorized"
                },
                BadRequestExcpetion =>
                new ErrorDetails()
                {
                    StatusCode  = context.Response.StatusCode,
                    Details     = ex.Message,
                    Message     = "UnAuthorized"
                },
                _ => new ErrorDetails()
                {
                    StatusCode  = 500,
                    Message     = "Internal Server Error",
                    Details     = ex.Message
                }
            };
          

            await context.Response.WriteAsJsonAsync(error);
        }
    }
}
