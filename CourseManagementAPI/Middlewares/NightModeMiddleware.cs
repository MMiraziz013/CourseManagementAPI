namespace CourseManagementAPI.Middlewares;

public class NightModeMiddleware : IMiddleware
{
    private readonly ILogger<NightModeMiddleware> _logger;

    public NightModeMiddleware(ILogger<NightModeMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            //TODO: Implement time checking condition
            int hourNow = DateTime.Now.Hour;
            if (hourNow >= 00 && hourNow <= 06)
            {
                throw new Exception("Requests unavailable in between 00:00 - 06:00 time");
            }
            
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            var statusCode = StatusCodes.Status403Forbidden;
            var title = "Services are unavailable at night";

            await context.Response.WriteAsJsonAsync(new
            {
                title,
                statusCode,
                detail = e.Message
            });
        }
        
    }
}