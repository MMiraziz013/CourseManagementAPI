using System.ComponentModel.DataAnnotations;

namespace CourseManagementAPI.Middlewares;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception occurred");

            var statusCode = StatusCodes.Status500InternalServerError;
            var title = "An unexpected error occurred.";

            if (ex is ArgumentException || ex is ValidationException)
            {
                statusCode = StatusCodes.Status400BadRequest;
                title = "Invalid input.";
            }
            else if (ex is ArgumentOutOfRangeException)
            {
                statusCode = StatusCodes.Status404NotFound;
                title = "The requested resource was not found.";
            }

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                title,
                statusCode,
                detail = ex.Message
            });
        }
    }
}