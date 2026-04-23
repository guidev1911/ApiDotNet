using System.Net;
using System.Text.Json;

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

            var response = new
            {
                status = (int)HttpStatusCode.InternalServerError,
                mensagem = "Erro interno no servidor",
                detalhe = ex.Message
            };

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }
    }
}