using System.Net;
using Clean.Application;
using Clean.Infrastructure;
using Clean.Infrastructure.Data;
using Dapper;
using Microsoft.AspNetCore.Diagnostics;
using Scalar.AspNetCore;

namespace CourseManagementAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // The service registrations are now handled by extension methods.
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddOpenApi();

        var app = builder.Build();

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
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error");
        }

        SqlMapper.AddTypeHandler(new DateOnlyHandler());
        app.UseHttpsRedirection();
        app.UseAuthorization();
        

        app.MapControllers();

        app.Map("/error", (HttpContext context) =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            var exception = exceptionHandlerPathFeature?.Error;

            var statusCode = (int)HttpStatusCode.InternalServerError;
            var title = "An unexpected error occurred.";

            if (exception is ArgumentException || exception is System.ComponentModel.DataAnnotations.ValidationException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                title = "Invalid input.";
            }
            else if (exception is ArgumentOutOfRangeException)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                title = "The requested resource was not found.";
            }

            return Results.Problem(
                title: title,
                statusCode: statusCode,
                detail: exception?.Message
            );
        });

        app.Run();
    }
}