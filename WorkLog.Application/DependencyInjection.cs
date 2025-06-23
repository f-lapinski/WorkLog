using Microsoft.Extensions.DependencyInjection;
using WorkLog.Application.Interfaces;
using WorkLog.Application.Services;

namespace WorkLog.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application services here
        services.AddScoped<IWorkdayService, WorkdayService>();

        return services;
    }
}