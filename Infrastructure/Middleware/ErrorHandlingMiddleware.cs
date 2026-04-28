using System.Text.Json;
using ApiDotNet.Application.Exceptions;

namespace ApiDotNet.Infrastructure.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
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
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            int statusCode = ex switch
            {
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };

            string mensagem = ex switch
            {
                UnauthorizedException => ex.Message,
                NotFoundException => ex.Message,
                _ => "Erro interno no servidor"
            };

            context.Response.StatusCode = statusCode;

            var response = new
            {
                status = statusCode,
                mensagem = mensagem
            };

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}