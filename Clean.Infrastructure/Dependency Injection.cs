using Clean.Application.Abstractions;
using Clean.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IAppDbContext, AppDbContext>();
        services.AddTransient<IStudentContext, StudentContext>();
        services.AddTransient<IPaymentContext, PaymentContext>();
        services.AddTransient<ICourseContext, CourseContext>();
        services.AddTransient<IAttendanceContext, AttendanceContext>();
        
        return services;
    }
}