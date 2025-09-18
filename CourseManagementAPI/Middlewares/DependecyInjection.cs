using Clean.Application.Abstractions;

namespace CourseManagementAPI.Middlewares;

public static class DependencyInjection
{
    public static IServiceCollection AddMiddlewares(this IServiceCollection services)
    {
        services.AddTransient<LoggingMiddleware>();
        // services.AddTransient<ApiAccessMiddleware>();
        services.AddTransient<ErrorHandlingMiddleware>();
        // services.AddTransient<AuthorizationMiddleware>();
        services.AddTransient<NightModeMiddleware>();
        services.AddScoped<IAppEnvironment, AppEnvironment>();

        return services;
    }
}