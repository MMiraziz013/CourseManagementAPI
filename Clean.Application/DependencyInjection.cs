using Clean.Application.Abstractions;
using Clean.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddTransient<IStudentService, StudentService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<ICourseService, CourseService>();
        services.AddTransient<IAttendanceService, AttendanceService>();
        
        return services;
    }
}