using GlobalExceptionMiddleware.Exceptions;
using System.Net;
using System.Text.Json;

namespace GlobalExceptionMiddleware.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception error)
            {
                await HandleExceptionAsync(context, error);
            }
        }
        private static async Task HandleExceptionAsync(HttpContext context, Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            switch (error)
            {
                case BadRequestException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case UnauthorizedException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            var result = JsonSerializer.Serialize(new { message = error?.Message });
            await response.WriteAsync(result);
        }
    }
}
