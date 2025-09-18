using Clean.Application.Abstractions;

namespace CourseManagementAPI.Middlewares;

using Microsoft.AspNetCore.Hosting;

public class AppEnvironment : IAppEnvironment
{
    private readonly IWebHostEnvironment _env;

    public AppEnvironment(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string WebRootPath => _env.WebRootPath;
}
