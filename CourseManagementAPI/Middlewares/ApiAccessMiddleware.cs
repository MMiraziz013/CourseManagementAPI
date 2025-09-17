namespace CourseManagementAPI.Middlewares;

public class ApiAccessMiddleware : IMiddleware
{
    private readonly string _apiKey;

    public ApiAccessMiddleware(IConfiguration configuration)
    {
        _apiKey = configuration["ApiKey"] ?? throw new InvalidOperationException("API key is not found");
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API key is missing");
            return;
        }

        if (!_apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Incorrect API key");
            return;
        }

        await next(context);
    }
}