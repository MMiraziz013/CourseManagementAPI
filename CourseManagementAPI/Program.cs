using System.Net;
using Clean.Application;
using Clean.Infrastructure;
using Clean.Infrastructure.Data;
using CourseManagementAPI.Middlewares;
using Dapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Scalar.AspNetCore;
using Serilog;

namespace CourseManagementAPI;

public class Program
{
    public static void Main(string[] args)
    {
        // Configurations of the Serilog before app builds
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        var builder = WebApplication.CreateBuilder(args);
        
        // Replacing the default .NET logger with Serilog
        builder.Host.UseSerilog();

        // Add services to the container.
        // The service registrations are now handled by extension methods.
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();
        builder.Services.AddMiddlewares();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Description = "Enter API Key into the field below",
                    Name = "X-API-KEY",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "ApiKeyScheme"
                }
            );

            // c.AddSecurityRequirement(new OpenApiSecurityRequirement
            // {
            //     {
            //         new OpenApiSecurityScheme
            //         {
            //             Reference = new OpenApiReference
            //             {
            //                 Type = ReferenceType.SecurityScheme,
            //                 Id = "ApiKey"
            //             },
            //             Scheme = "ApiKeyScheme",
            //             Name = "X-API-KEY",
            //             In = ParameterLocation.Header
            //         },
            //         new List<string>()
            //     }
            // });
            
                
        });
        builder.Services.AddOpenApi();

        var app = builder.Build();

        app.UseMiddleware<ErrorHandlingMiddleware>();
        //TODO: Uncomment after implementing NightModeMiddleware
        //app.UseMiddleware<NightModeMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();


            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty; // Set the route prefix to an empty string
            });

            app.MapScalarApiReference(options =>
            {
                options.WithOpenApiRoutePattern("/swagger/v1/swagger.json");
            });
        }

        SqlMapper.AddTypeHandler(new DateOnlyHandler());
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseStaticFiles();
        // app.UseMiddleware<ApiAccessMiddleware>();
        // app.UseMiddleware<AuthorizationMiddleware>();
        app.UseMiddleware<NightModeMiddleware>();
        app.MapControllers();

        app.Run();
    }
}