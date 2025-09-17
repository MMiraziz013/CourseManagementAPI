namespace CourseManagementAPI.Middlewares;

public class AuthorizationMiddleware : IMiddleware
{
    private readonly ILogger<AuthorizationMiddleware> _logger;

    public AuthorizationMiddleware(ILogger<AuthorizationMiddleware> logger)
    {
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // 1. Read the custom header
        var role = context.Request.Headers["X-User-Role"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(role))
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                title = "Forbidden",
                statusCode = 403,
                detail = "X-User-Role header is missing."
            });
            return;
        }

        // 2. Check Admin (full access)
        if (role.Equals("Admin", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }

        // 3. Check User (only GET allowed)
        if (role.Equals("User", StringComparison.OrdinalIgnoreCase))
        {
            if (HttpMethods.IsGet(context.Request.Method))
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new
            {
                title = "Forbidden",
                statusCode = 403,
                detail = "User role is only allowed to perform GET requests."
            });
            return;
        }

        // 4. Any other role → forbidden
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        await context.Response.WriteAsJsonAsync(new
        {
            title = "Forbidden",
            statusCode = 403,
            detail = $"Role '{role}' is not allowed."
        });
    }
}