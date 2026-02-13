using System.Net;
using System.Text.Json;

namespace TrabukaApi.Helpers
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
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
                _logger.LogError(ex, "Erro não tratado: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                Error = new
                {
                    Message = "Ocorreu um erro interno no servidor.",
                    Details = exception.Message,
                    Timestamp = DateTime.UtcNow,
                    TraceId = context.TraceIdentifier
                }
            };

            switch (exception)
            {
                case ArgumentException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        Error = new
                        {
                            Message = "Parâmetros inválidos fornecidos.",
                            Details = exception.Message,
                            Timestamp = DateTime.UtcNow,
                            TraceId = context.TraceIdentifier
                        }
                    };
                    break;

                case InvalidOperationException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        Error = new
                        {
                            Message = "Operação inválida.",
                            Details = exception.Message,
                            Timestamp = DateTime.UtcNow,
                            TraceId = context.TraceIdentifier
                        }
                    };
                    break;

                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        Error = new
                        {
                            Message = "Acesso não autorizado.",
                            Details = exception.Message,
                            Timestamp = DateTime.UtcNow,
                            TraceId = context.TraceIdentifier
                        }
                    };
                    break;

                case KeyNotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    response = new
                    {
                        Error = new
                        {
                            Message = "Recurso não encontrado.",
                            Details = exception.Message,
                            Timestamp = DateTime.UtcNow,
                            TraceId = context.TraceIdentifier
                        }
                    };
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            var json = JsonSerializer.Serialize(response, options);
            await context.Response.WriteAsync(json);
        }
    }

    // Classe de extensão para facilitar o registro do middleware
    public static class GlobalExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionHandler>();
        }
    }
} 